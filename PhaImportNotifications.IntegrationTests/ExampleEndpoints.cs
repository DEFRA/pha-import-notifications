using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PhaImportNotifications.Endpoints;

namespace PhaImportNotifications.IntegrationTests;

public class ExampleEndpoints(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetHelloWorldReturnsHelloWorld()
    {
        var client = factory.CreateClient();
        var result = await client.GetFromJsonAsync<PhaEndpoints.HelloWorldResponse>("/hello/world");
        result.Should().BeEquivalentTo(new PhaEndpoints.HelloWorldResponse());
    }
}
