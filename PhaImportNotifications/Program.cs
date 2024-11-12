using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Microsoft.OpenApi.Models;
using PhaImportNotifications.Endpoints;
using PhaImportNotifications.SwashbuckleFilters;
using PhaImportNotifications.Utils;
using PhaImportNotifications.Utils.Http;
using PhaImportNotifications.Utils.Logging;
using PhaImportNotifications.Utils.Mongo;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.ReDoc;
using Swashbuckle.AspNetCore.SwaggerGen;

var app = CreateWebApplication(args);
await app.RunAsync();

[ExcludeFromCodeCoverage]
static WebApplication CreateWebApplication(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureWebApplication(builder);

    return BuildWebApplication(builder);
}

[ExcludeFromCodeCoverage]
static void ConfigureWebApplication(WebApplicationBuilder builder)
{
    builder.Configuration.AddEnvironmentVariables();

    var logger = ConfigureLogging(builder);

    // Load certificates into Trust Store - Note must happen before Mongo and Http client connections
    builder.Services.AddCustomTrustStore(logger);

    ConfigureMongoDb(builder);
    ConfigureEndpoints(builder);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddServer(
            new OpenApiServer
            {
                Description = "The Open Government Licence (OGL) Version 3",
                Url = "https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3",
            }
        );
        c.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var methodInfo) ? methodInfo.Name : null);
        c.EnableAnnotations();
        c.IncludeXmlComments(Assembly.GetExecutingAssembly());
        c.OperationFilter<AddSwashbuckleErrorResponses>();
        c.OperationFilter<AddSwashbuckleHeaders>();
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
                        "https://www.gov.uk/government/organisations/department-for-environment-food-rural-affairs"
                    ),
                },
                Title = "PHA Import Notifications",
                Version = "v1",
            }
        );
    });
    builder.Services.AddHttpClient();

    // calls outside the platform should be done using the named 'proxy' http client.
    builder.Services.AddHttpProxyClient(logger);

    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
}

[ExcludeFromCodeCoverage]
static Logger ConfigureLogging(WebApplicationBuilder builder)
{
    builder.Logging.ClearProviders();
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.With<LogLevelMapper>()
        .CreateLogger();
    builder.Logging.AddSerilog(logger);
    logger.Information("Starting application");
    return logger;
}

[ExcludeFromCodeCoverage]
static void ConfigureMongoDb(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IMongoDbClientFactory>(_ => new MongoDbClientFactory(
        builder.Configuration.GetValue<string>("Mongo:DatabaseUri")!,
        builder.Configuration.GetValue<string>("Mongo:DatabaseName")!
    ));
}

[ExcludeFromCodeCoverage]
static void ConfigureEndpoints(WebApplicationBuilder builder)
{
    builder.Services.AddHealthChecks();
}

[ExcludeFromCodeCoverage]
static WebApplication BuildWebApplication(WebApplicationBuilder builder)
{
    var app = builder.Build();

    app.MapHealthChecks("/health");
    app.UsePhaEndpoints();

    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/.well-known/openapi/{documentName}/openapi.json";
    });
    app.UseReDoc(options =>
    {
        options.ConfigObject = new ConfigObject { ExpandResponses = "200" };
        options.DocumentTitle = "PHA Import Notifications";
        options.RoutePrefix = "redoc";
        options.SpecUrl = "/.well-known/openapi/v1/openapi.json";
    });

    return app;
}
