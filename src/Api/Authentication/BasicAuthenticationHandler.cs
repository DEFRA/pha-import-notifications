using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Defra.PhaImportNotifications.Api.Authentication;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    IOptionsMonitor<BasicAuthOptions> basicAuthenticationOptions,
    IOptions<AclOptions> aclOptions,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly AclOptions _aclOptions = aclOptions.Value;
    private readonly BasicAuthOptions _authOptions = basicAuthenticationOptions.CurrentValue;

    private static (string, string)? GetCredentials(StringValues header)
    {
        if (StringValues.IsNullOrEmpty(header))
            return null;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(header!);
            if (authHeader.Parameter is null)
                return null;

            return BasicAuthHelper.FromBasicAuth(authHeader.Parameter);
        }
        catch
        {
            return null;
        }
    }

    private AuthenticationTicket CreateAuthenticationTicket(string username, List<string> bcps)
    {
        var bcpClaims = bcps.Select(bcp => new Claim(PhaClaimTypes.Bcp, bcp));
        var claims = bcpClaims.Concat([new Claim(ClaimTypes.Name, username)]);

        var identity = new ClaimsIdentity(claims, "Basic");
        return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!_authOptions.Enabled)
            return Task.FromResult(
                AuthenticateResult.Success(CreateAuthenticationTicket("LocalDev", ["bcp1", "bcp2"]))
            );

        var credentials = GetCredentials(Request.Headers.Authorization);
        if (credentials is null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

        var (username, password) = credentials.Value;
        if (username != _authOptions.Username || password != _authOptions.Password)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

        return Task.FromResult(
            !_aclOptions.Clients.TryGetValue(username, out var client)
                ? AuthenticateResult.Fail("Unknown Client")
                : AuthenticateResult.Success(CreateAuthenticationTicket(username, client.Bcps))
        );
    }
}
