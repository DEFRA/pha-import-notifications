using System.Reflection;
using System.Text;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Endpoints;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Extensions;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.OpenApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Api.Utils;
using Defra.PhaImportNotifications.Api.Utils.Logging;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    var app = CreateWebApplication(args);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}

return;

static WebApplication CreateWebApplication(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureWebApplication(builder, args);

    return BuildWebApplication(builder);
}

static void ConfigureWebApplication(WebApplicationBuilder builder, string[] args)
{
    var generatingOpenApiFromCli = Assembly.GetEntryAssembly()?.GetName().Name == "dotnet-swagger";
    var integrationTest = args.Contains("--integrationTest=true");
    var cdpAppSettingsOptional = generatingOpenApiFromCli || integrationTest;

    builder.Configuration.AddJsonFile(
        $"appsettings.cdp.{Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToLower()}.json",
        cdpAppSettingsOptional
    );
    builder.Configuration.AddEnvironmentVariables();

    // Load certificates into Trust Store - Note must happen before Mongo and Http client connections
    builder.Services.AddCustomTrustStore();

    // Configure logging to use the CDP Platform standards.
    builder.Services.AddHttpContextAccessor();
    if (!integrationTest)
        // Configuring Serilog below wipes out the framework logging
        // so we don't execute the following when the host is running
        // within an integration test
        builder.Host.UseSerilog(CdpLogging.Configuration);

    builder.Services.AddPhaJwtAuthentication();

    // This adds default rate limiter, total request timeout, retries, circuit breaker and timeout per attempt
    builder.Services.ConfigureHttpClientDefaults(options => options.AddStandardResilienceHandler());
    builder.Services.ConfigureHttpJsonOptions(options => SerializerOptions.Configure(options));
    // This is needed for Swashbuckle and Minimal APIs
    builder.Services.Configure<JsonOptions>(options => SerializerOptions.Configure(options));
    builder.Services.TryAddTransient<ISerializerDataContractResolver>(sp => new JsonSerializerDataContractResolver(
        sp.GetRequiredService<IOptions<JsonOptions>>().Value.SerializerOptions
    ));
    // /This is needed for Swashbuckle and Minimal APIs
    builder.Services.AddProblemDetails();
    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddServer(
            new OpenApiServer
            {
                Url = "https://" + (builder.Configuration.GetValue<string>("OpenApi:Host") ?? "localhost"),
            }
        );
        c.AddSecurityDefinition(
            "oAuth",
            new OpenApiSecurityScheme
            {
                Description = "RFC8725 Compliant JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri(
                            "https://"
                                + (builder.Configuration.GetValue<string>("OpenApi:AuthEndpoint") ?? "localhost")
                                + "/oauth2/token"
                        ),
                    },
                },
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oAuth" },
                    },
                    []
                },
            }
        );
        c.IncludeXmlComments(Assembly.GetExecutingAssembly());
        c.IncludeXmlComments(typeof(ImportNotification).Assembly);
        c.DocumentFilter<TagsDocumentFilter>();
        c.SchemaFilter<DescriptionSchemaFilter>();
        c.UseAllOfToExtendReferenceSchemas();
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Description = """
                The PHA API enforces a rate limit on requests to avoid excessive usage which, if reached, will return a 429 status code.
                Please get in touch if you encounter this limit.
                """,
                Contact = new OpenApiContact
                {
                    Email = "tbc@defra.gov.uk",
                    Name = "DEFRA",
                    Url = new Uri(
#pragma warning disable S1075
                        "https://www.gov.uk/government/organisations/department-for-environment-food-rural-affairs"
#pragma warning restore S1075
                    ),
                },
                Title = "PHA Import Notifications",
                Version = "v1",
            }
        );
    });
    builder.Services.AddHttpClient();
    builder.Services.AddHeaderPropagation(options =>
    {
        var traceHeader = builder.Configuration.GetValue<string>("TraceHeader");
        if (!string.IsNullOrWhiteSpace(traceHeader))
            options.Headers.Add(traceHeader);
    });
    builder.Services.AddOptions<AclOptions>().BindConfiguration("Acl").ValidateOptions(!generatingOpenApiFromCli);
    builder.Services.AddOptions<BtmsOptions>().BindConfiguration("Btms").ValidateOptions(!generatingOpenApiFromCli);
    builder.Services.AddJsonApiClient(
        (sp, options) =>
        {
            var btmsOptions = sp.GetRequiredService<IOptions<BtmsOptions>>().Value;

            options.BaseUrl = btmsOptions.BaseUrl;
            options.BasicAuthCredential = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{btmsOptions.Username}:{btmsOptions.Password}")
            );
        }
    );
    builder.Services.AddTransient<IBtmsService, BtmsService>();
}

static WebApplication BuildWebApplication(WebApplicationBuilder builder)
{
    var app = builder.Build();

    app.UseHeaderPropagation();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks("/health");
    app.MapExampleEndpoints();
    app.MapImportNotificationsEndpoints();

    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/.well-known/openapi/{documentName}/openapi.json";
    });
    app.UseReDoc(options =>
    {
        options.ConfigObject.ExpandResponses = "200";
        options.DocumentTitle = "PHA Import Notifications";
        options.RoutePrefix = "redoc";
        options.SpecUrl = "/.well-known/openapi/v1/openapi.json";
    });

    app.UseStatusCodePages();
    app.UseExceptionHandler(
        new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var error = exceptionHandlerFeature?.Error;
                string? detail = null;

                if (error is BadHttpRequestException badHttpRequestException)
                {
                    context.Response.StatusCode = badHttpRequestException.StatusCode;
                    detail = badHttpRequestException.Message;
                }

                if (context.RequestServices.GetRequiredService<IProblemDetailsService>() is { } problemDetailsService)
                    await problemDetailsService.WriteAsync(
                        new ProblemDetailsContext
                        {
                            HttpContext = context,
                            AdditionalMetadata = exceptionHandlerFeature?.Endpoint?.Metadata,
                            ProblemDetails = { Status = context.Response.StatusCode, Detail = detail },
                        }
                    );
                else if (ReasonPhrases.GetReasonPhrase(context.Response.StatusCode) is { } reasonPhrase)
                    await context.Response.WriteAsync(reasonPhrase);
            },
        }
    );

    return app;
}

#pragma warning disable S2094
namespace Defra.PhaImportNotifications.Api
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program;
}
#pragma warning restore S2094
