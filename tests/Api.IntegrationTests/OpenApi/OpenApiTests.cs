using Microsoft.AspNetCore.Mvc.Testing;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.OpenApi;

public class OpenApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task OpenApi_VerifyAsExpected()
    {
        var client = factory.CreateClient();
        var response = await client.GetStringAsync("/.well-known/openapi/v1/openapi.json");

        await VerifyJson(response);
    }
}