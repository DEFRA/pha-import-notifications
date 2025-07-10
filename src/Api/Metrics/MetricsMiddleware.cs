using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http.Metadata;

namespace Defra.PhaImportNotifications.Api.Metrics;

[ExcludeFromCodeCoverage]
public class MetricsMiddleware(RequestMetrics requestMetrics) : IMiddleware
{
    private static readonly string[] s_startsWith = ["/apple-", "/favicon", "/redoc", "/.well-known/openapi"];

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var startingTimestamp = TimeProvider.System.GetTimestamp();

        await next(context);

        var routeMetadata = context.GetEndpoint()?.Metadata.GetMetadata<IRouteDiagnosticsMetadata>();
        var path = routeMetadata?.Route;

        if (IgnoreRequest(path))
            return;

        requestMetrics.RequestCompleted(
            path!,
            context.Request.Method,
            context.Response.StatusCode,
            TimeProvider.System.GetElapsedTime(startingTimestamp)
        );
    }

    private static bool IgnoreRequest(string? path) =>
        !string.IsNullOrWhiteSpace(path) && s_startsWith.Any(path.StartsWith);
}
