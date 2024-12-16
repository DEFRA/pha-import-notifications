using System.Diagnostics.CodeAnalysis;
using Elastic.Serilog.Enrichers.Web;
using Serilog;

namespace Defra.PhaImportNotifications.Api.Utils.Logging;

[ExcludeFromCodeCoverage]
public static class CdpLogging
{
    public static void Configuration(HostBuilderContext ctx, LoggerConfiguration config)
    {
        var httpAccessor = ctx.Configuration.Get<HttpContextAccessor>();
        var traceIdHeader = ctx.Configuration.GetValue<string>("TraceHeader");
        var serviceVersion = Environment.GetEnvironmentVariable("SERVICE_VERSION") ?? "";

        config
            .ReadFrom.Configuration(ctx.Configuration)
            .Enrich.WithEcsHttpContext(httpAccessor!)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("service.version", serviceVersion);

        if (traceIdHeader != null)
        {
            config.Enrich.WithCorrelationId(traceIdHeader);
        }
    }
}