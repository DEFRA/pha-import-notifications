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
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
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

    private AuthenticationTicket CreateAuthenticationTicket(string username)
    {
        var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, username)], "Basic");
        return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!_authOptions.Enabled)
            return Task.FromResult(AuthenticateResult.Success(CreateAuthenticationTicket("LocalDev")));

        var credentials = GetCredentials(Request.Headers.Authorization);
        if (credentials is null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

        var (username, password) = credentials.Value;
        if (username != _authOptions.Username || password != _authOptions.Password)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

        return Task.FromResult(AuthenticateResult.Success(CreateAuthenticationTicket(username)));
    }
}
