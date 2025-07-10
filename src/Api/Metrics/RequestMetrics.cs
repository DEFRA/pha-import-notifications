using System.Diagnostics;
using System.Diagnostics.Metrics;
using Amazon.CloudWatch.EMF.Model;

namespace Defra.PhaImportNotifications.Api.Metrics;

public class RequestMetrics
{
    private readonly Counter<long> _requestsReceived;
    private readonly Histogram<double> _requestDuration;

    private static class RequestTags
    {
        public const string Method = nameof(Method);
        public const string Path = nameof(Path);
        public const string StatusCode = nameof(StatusCode);
    }

    public RequestMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MetricsConstants.MetricNames.MeterName);

        _requestsReceived = meter.CreateCounter<long>("http.server.request", nameof(Unit.COUNT), "Count of Requests");

        _requestDuration = meter.CreateHistogram<double>(
            "http.server.request.duration",
            nameof(Unit.MILLISECONDS),
            "Duration of request"
        );
    }

    public void RequestCompleted(string requestPath, string httpMethod, int statusCode, TimeSpan duration)
    {
        _requestsReceived.Add(1, BuildTags(requestPath, httpMethod, statusCode));
        _requestDuration.Record(duration.TotalMilliseconds, BuildTags(requestPath, httpMethod, statusCode));
    }

    private static TagList BuildTags(string requestPath, string httpMethod, int statusCode) =>
        new()
        {
            { RequestTags.Path, requestPath },
            { RequestTags.Method, httpMethod },
            { RequestTags.StatusCode, statusCode },
        };
}
