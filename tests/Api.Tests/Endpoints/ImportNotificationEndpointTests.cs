using Api.Endpoints;
using FluentAssertions;

namespace Api.Tests.Endpoints;

public class ImportNotificationEndpointTests
{
    [Fact]
    public void GetResult()
    {
        var result = ImportNotificationEndpoint.Get("TEST");
        result.Should().NotBeNull();
    }
}
