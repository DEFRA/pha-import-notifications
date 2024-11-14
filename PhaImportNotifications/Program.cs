using System.Reflection;
using Microsoft.OpenApi.Models;
using PhaImportNotifications.Endpoints;
using PhaImportNotifications.SwashbuckleFilters;
using PhaImportNotifications.Utils;
using PhaImportNotifications.Utils.Http;
using PhaImportNotifications.Utils.Logging;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.ReDoc;
using Swashbuckle.AspNetCore.SwaggerGen;

var app = CreateWebApplication(args);
await app.RunAsync();

static WebApplication CreateWebApplication(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureWebApplication(builder);

    return BuildWebApplication(builder);
}

static void ConfigureWebApplication(WebApplicationBuilder builder)
{
    builder.Configuration.AddEnvironmentVariables();

    var logger = ConfigureLogging(builder);

    // Load certificates into Trust Store - Note must happen before Mongo and Http client connections
    builder.Services.AddCustomTrustStore(logger);

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

static void ConfigureEndpoints(WebApplicationBuilder builder)
{
    builder.Services.AddHealthChecks();
}

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

#pragma warning disable S2094
namespace PhaImportNotifications
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class Program;
}
#pragma warning restore S2094
