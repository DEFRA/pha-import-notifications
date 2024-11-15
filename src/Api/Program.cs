using Api.Endpoints;
using Api.OpenApi;
using Api.Utils;
using Api.Utils.Http;
using Api.Utils.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Swashbuckle.AspNetCore.ReDoc;

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

    ConfigureWebApplication(builder);

    return BuildWebApplication(builder);
}

static void ConfigureWebApplication(WebApplicationBuilder builder)
{
    builder.Configuration.AddEnvironmentVariables();

    var logger = ConfigureLogging(builder);

    // Load certificates into Trust Store - Note must happen before Mongo and Http client connections
    builder.Services.AddCustomTrustStore(logger);

    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer(
            (document, _, _) =>
            {
                document.Info = new OpenApiInfo
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
                };

                document.Servers = new List<OpenApiServer>
                {
                    new()
                    {
                        Description = "The Open Government Licence (OGL) Version 3",
                        Url = "https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3",
                    },
                };

                return Task.CompletedTask;
            }
        );

        options.AddOperationTransformer<ErrorResponsesTransformer>();
        options.AddOperationTransformer<HeadersTransformer>();
        options.AddSchemaTransformer<XmlDocsSchemaTransformer>();
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

static WebApplication BuildWebApplication(WebApplicationBuilder builder)
{
    var app = builder.Build();

    app.MapHealthChecks("/health");
    app.MapPhaEndpoints();
    app.MapOpenApi("/.well-known/openapi/{documentName}/openapi.json");
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
namespace Api
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class Program;
}
#pragma warning restore S2094
