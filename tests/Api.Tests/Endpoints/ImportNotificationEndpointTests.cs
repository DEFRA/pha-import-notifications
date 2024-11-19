using Defra.PhaImportNotifications.Api.Endpoints;
using FluentAssertions;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints;

public class ImportNotificationEndpointTests
{
    [Fact]
    public void GetResult()
    {
        var result = ImportNotificationEndpoint.Get("TEST");
        result.Should().NotBeNull();
    }
}
