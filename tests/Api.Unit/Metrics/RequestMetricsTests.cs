using System.Diagnostics.Metrics;
using Defra.PhaImportNotifications.Api.Metrics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics.Testing;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Metrics;

public class RequestMetricsTests
{
    [Fact]
    public void When_message_received_Then_counter_is_incremented()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMetrics();
        var mf = serviceCollection.BuildServiceProvider().GetRequiredService<IMeterFactory>();

        var messagesReceivedCollector = new MetricCollector<long>(
            mf,
            MetricsConstants.MetricNames.MeterName,
            "http.server.request"
        );

        var messagesReceivedDurationCollector = new MetricCollector<double>(
            mf,
            MetricsConstants.MetricNames.MeterName,
            "http.server.request.duration"
        );

        var metrics = new RequestMetrics(mf);

        metrics.RequestCompleted("TestMessage1", "/test-request-path-1", 200, TimeSpan.FromSeconds(2));

        var receivedMeasurements = messagesReceivedCollector.GetMeasurementSnapshot();

        receivedMeasurements.Count.Should().Be(1);
        receivedMeasurements[0].Value.Should().Be(1);

        receivedMeasurements[0].ContainsTags("Path").Should().BeTrue();
        receivedMeasurements[0].Tags["Path"].Should().Be("TestMessage1");

        receivedMeasurements[0].ContainsTags("Method").Should().BeTrue();
        receivedMeasurements[0].Tags["Method"].Should().Be("/test-request-path-1");

        receivedMeasurements[0].ContainsTags("StatusCode").Should().BeTrue();
        receivedMeasurements[0].Tags["StatusCode"]?.Should().Be(200);

        var receivedDurationMeasurements = messagesReceivedDurationCollector.GetMeasurementSnapshot();

        receivedDurationMeasurements.Count.Should().Be(1);
        receivedDurationMeasurements[0].Value.Should().Be(2000);
    }
}
