using System.Net.Http.Json;
using Api.Endpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.IntegrationTests;

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