using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using Argon;
using Defra.PhaImportNotifications.Api.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Xunit.Abstractions;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints;

public class EndpointTestBase : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;

    protected readonly VerifySettings _verifySettings;

    protected EndpointTestBase(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper)
    {
        _factory = factory;
        _factory.OutputHelper = outputHelper;
        _factory.ConfigureHostConfiguration = ConfigureHostConfiguration;

        _verifySettings = new VerifySettings();
        _verifySettings.UseStrictJson();
        _verifySettings.DontScrubGuids();
        _verifySettings.DontScrubDateTimes();
        _verifySettings.AddExtraSettings(settings => settings.DefaultValueHandling = DefaultValueHandling.Include);
        _verifySettings.UseDirectory("Verified");
    }

    /// <summary>
    /// Use this to inject configuration before Host is created.
    /// </summary>
    /// <param name="config"></param>
    protected virtual void ConfigureHostConfiguration(IConfigurationBuilder config) { }

    /// <summary>
    /// Use this to override DI services.
    /// </summary>
    /// <param name="services"></param>
    protected virtual void ConfigureTestServices(IServiceCollection services) { }

    protected HttpClient CreateClient(string clientId = "pha")
    {
        var builder = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(ConfigureTestServices);
        });

        var client = builder.CreateClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateJwt(clientId));

        return client;
    }

    private static string GenerateJwt(string clientId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId),
            new Claim(PhaClaimTypes.ClientId, clientId),
        };

        var rand = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(rand);

        var token = new JwtSecurityToken(
            issuer: "Local",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(rand), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
