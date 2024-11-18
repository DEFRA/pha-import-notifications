using Api.Endpoints;
using FluentAssertions;

namespace Api.Tests.Endpoints;

public class ImportNotificationUpdatesEndpointTests
{
    [Fact]
    public void GetAllUpdatedReturnsAResult()
    {
        var result = ImportNotificationUpdatesEndpoint.GetAllUpdated("", 0, 10, DateTime.Now, DateTime.Now);
        result.Should().NotBeNull();
    }
}
