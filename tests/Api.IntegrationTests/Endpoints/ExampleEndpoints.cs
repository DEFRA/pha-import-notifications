using System.Net.Http.Json;
using FluentAssertions;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class ExampleEndpoints(TestWebApplicationFactory<Program> factory, ITestOutputHelper outputHelper)
    : EndpointTestBase<Program>(factory, outputHelper)
{
    [Fact]
    public async Task GetHelloWorldReturnsHelloWorld()
    {
        var client = CreateClient();

        var result = await client.GetFromJsonAsync<Api.Endpoints.ExampleEndpoints.HelloWorldResponse>("/hello/world");

        result.Should().BeEquivalentTo(new Api.Endpoints.ExampleEndpoints.HelloWorldResponse());
    }
}
