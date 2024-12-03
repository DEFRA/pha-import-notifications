using System.Reflection;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Endpoints;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotificationsUpdates;
using Defra.PhaImportNotifications.Api.Extensions;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.OpenApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Api.Utils;
using Defra.PhaImportNotifications.Api.Utils.Http;
using Defra.PhaImportNotifications.Api.Utils.Logging;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.SwaggerGen;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    var app = CreateWebApplication(args);
    var btmsOptions = app.Services.GetRequiredService<IOptions<BtmsOptions>>().Value;

    if (btmsOptions.StubEnabled)
        app.Services.GetRequiredService<WireMockBtmsService>().Start();

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

    ConfigureWebApplication(builder);

    return BuildWebApplication(builder);
}

static void ConfigureWebApplication(WebApplicationBuilder builder)
{
    builder.Configuration.AddEnvironmentVariables();

    var generatingOpenApiFromCli = Assembly.GetEntryAssembly()?.GetName().Name == "dotnet-swagger";

    var logger = ConfigureLogging(builder);

    // Load certificates into Trust Store - Note must happen before Mongo and Http client connections
    builder.Services.AddCustomTrustStore(logger);

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
        c.AddServer(new OpenApiServer { Url = "https://localhost" });
        c.IncludeXmlComments(Assembly.GetExecutingAssembly());
        c.IncludeXmlComments(typeof(ImportNotification).Assembly);
        c.DocumentFilter<TagsDocumentFilter>();
        c.SchemaFilter<DescriptionSchemaFilter>();
        c.UseAllOfToExtendReferenceSchemas();
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Description = "TBC",
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
    builder.Services.AddOptions<BtmsOptions>().BindConfiguration("Btms").ValidateOptions(!generatingOpenApiFromCli);
    builder.Services.AddJsonApiClient(
        sp => sp.GetRequiredService<IOptions<BtmsOptions>>().Value.BaseUrl,
        sp => sp.GetRequiredService<IOptions<BtmsOptions>>().Value.BasicAuthCredential
    );
    builder.Services.AddTransient<IBtmsService, StubBtmsService>();
    builder.Services.AddSingleton<WireMockBtmsService>();

    // calls outside the platform should be done using the named 'proxy' http client.
    builder.Services.AddHttpProxyClient(logger);
}

static Logger ConfigureLogging(WebApplicationBuilder builder)
{
    builder.Logging.ClearProviders();

    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.With<LogLevelMapper>()
        .Enrich.WithProperty("service.version", Environment.GetEnvironmentVariable("SERVICE_VERSION"))
        .CreateLogger();

    builder.Logging.AddSerilog(logger);

    logger.Information("Starting application");

    return logger;
}

static WebApplication BuildWebApplication(WebApplicationBuilder builder)
{
    var app = builder.Build();

    app.MapHealthChecks("/health");
    app.MapExampleEndpoints();
    app.MapImportNotificationsEndpoints();
    app.MapImportNotificationsUpdatesEndpoints();

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
