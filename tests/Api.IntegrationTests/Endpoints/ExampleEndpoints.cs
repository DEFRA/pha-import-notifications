using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class ExampleEndpoints(WebApplicationFactory<Program> factory) : EndpointTestBase<Program>(factory)
{
    [Fact]
    public async Task GetHelloWorldReturnsHelloWorld()
    {
        var client = CreateClient();

        var result = await client.GetFromJsonAsync<Api.Endpoints.ExampleEndpoints.HelloWorldResponse>("/hello/world");

        result.Should().BeEquivalentTo(new Api.Endpoints.ExampleEndpoints.HelloWorldResponse());
    }
}
