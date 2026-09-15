using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using ClaimTypes = Defra.PhaImportNotifications.Api.Authentication.ClaimTypes;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints.Token;

public class TokenTests(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
{
    private const string ClientId = "local-dev-test";

    [Fact]
    public async Task Token_WhenNotLocalDevelopment_ShouldNotBeFound()
    {
        var response = await factory.CreateClient().PostAsync("/oauth2/token", Form(ClientId));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Token_WhenLocalDevelopment_ShouldReturnToken()
    {
        var response = await CreateLocalDevClient().PostAsync("/oauth2/token", Form(ClientId));

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        json.RootElement.GetProperty("token_type").GetString().Should().Be("Bearer");
        json.RootElement.GetProperty("expires_in").GetInt32().Should().BePositive();

        var accessToken = json.RootElement.GetProperty("access_token").GetString();
        accessToken.Should().NotBeNullOrEmpty();

        var token = new JsonWebToken(accessToken!);

        token.GetClaim(ClaimTypes.ClientId).Value.Should().Be(ClientId);
        token.Subject.Should().Be(ClientId);
        token.ValidTo.Should().BeAfter(DateTime.UtcNow);
    }

    [Theory]
    [InlineData("authorization_code")]
    [InlineData("password")]
    [InlineData("")]
    public async Task Token_WhenGrantTypeIsUnsupported_ShouldBeBadRequest(string grantType)
    {
        var response = await CreateLocalDevClient()
            .PostAsync("/oauth2/token", Form(("grant_type", grantType), ("client_id", ClientId)));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Token_WhenClientIdIsMissing_ShouldBeBadRequest(string? clientId)
    {
        var form = clientId is null
            ? Form(("grant_type", "client_credentials"))
            : Form(("grant_type", "client_credentials"), ("client_id", clientId));

        var response = await CreateLocalDevClient().PostAsync("/oauth2/token", form);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Token_WhenClientIdNotInAcl_ShouldBeBadRequest()
    {
        var response = await CreateLocalDevClient().PostAsync("/oauth2/token", Form("not-in-the-acl"));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private HttpClient CreateLocalDevClient() =>
        factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
                builder.UseSetting("AWS_EMF_ENABLED", "false");
                builder.UseSetting("LocalDevToken:Enabled", "true");
                builder.UseSetting($"Acl:Clients:{ClientId}:Bcps:0", "*");
                builder.UseSetting($"Acl:Clients:{ClientId}:ChedTypes:0", "*");
            })
            .CreateClient();

    private static FormUrlEncodedContent Form(string clientId) =>
        Form(("grant_type", "client_credentials"), ("client_id", clientId));

    private static FormUrlEncodedContent Form(params (string Key, string Value)[] values) =>
        new(values.Select(v => new KeyValuePair<string, string>(v.Key, v.Value)));
}
