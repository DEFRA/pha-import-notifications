using System.Diagnostics.CodeAnalysis;

namespace Defra.PhaImportNotifications.Api.Metrics;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseEmfExporter(this IApplicationBuilder builder, string ns)
    {
        var config = builder.ApplicationServices.GetRequiredService<IConfiguration>();
        var enabled = config.GetValue("AWS_EMF_ENABLED", true);

        if (enabled)
        {
            var nsc = config.GetValue<string>("AWS_EMF_NAMESPACE");
            var env = config.GetValue<string>("AWS_EMF_ENVIRONMENT") ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nsc) && env.Equals("Local"))
                nsc = ns;

            if (string.IsNullOrWhiteSpace(nsc))
                throw new InvalidOperationException("AWS_EMF_NAMESPACE is not set but metrics are enabled");

            EmfExporter.Init(builder.ApplicationServices.GetRequiredService<ILoggerFactory>(), nsc);
        }

        return builder;
    }
}
