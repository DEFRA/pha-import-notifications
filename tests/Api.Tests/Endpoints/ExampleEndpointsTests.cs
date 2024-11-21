using Defra.PhaImportNotifications.Api.Endpoints;
using FluentAssertions;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints;

public class ExampleEndpointsTests
{
    [Fact]
    public void GetHelloWorldReturnsHelloWorld()
    {
        var result = ExampleEndpoints.HelloWorld();
        result.Should().BeEquivalentTo(new ExampleEndpoints.HelloWorldResponse());
    }
}
