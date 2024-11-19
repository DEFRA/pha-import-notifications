using Api.Endpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Api.Tests.Endpoints;

public class ImportNotificationUpdatesEndpointTests
{
    [Fact]
    public void GetAllUpdatedReturnsAResult()
    {
        var result = ImportNotificationUpdatesEndpoint.GetAllUpdated(
            "",
            0,
            10,
            DateTime.Now,
            DateTime.Now,
            new DefaultHttpContext() { Request = { Host = new HostString("localhost"), Scheme = "http" } }
        );
        result.Should().NotBeNull();
    }
}
