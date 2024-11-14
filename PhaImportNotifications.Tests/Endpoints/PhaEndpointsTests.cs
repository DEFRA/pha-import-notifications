using FluentAssertions;
using PhaImportNotifications.Endpoints;

namespace PhaImportNotifications.Tests.Endpoints;

public class PhaEndpointsTests
{
    [Fact]
    public void GetHelloWorldReturnsHelloWorld()
    {
        var result = PhaEndpoints.HelloWorld();
        result.Should().BeEquivalentTo(new PhaEndpoints.HelloWorldResponse());
    }
}
