using Defra.PhaImportNotifications.Api.Endpoints;
using FluentAssertions;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints;

public class PhaEndpointsTests
{
    [Fact]
    public void GetHelloWorldReturnsHelloWorld()
    {
        var result = PhaEndpoints.HelloWorld();
        result.Should().BeEquivalentTo(new PhaEndpoints.HelloWorldResponse());
    }
}
