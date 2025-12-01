using System.Security.Claims;
using System.Text.Encodings.Web;
using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.Authentication;

public class PhaJwtAuthenticationHandler(
    IOptionsMonitor<JwtBearerOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<AclOptions> aclOptions
) : JwtBearerHandler(options, logger, encoder)
{
    private readonly JwtBearerEvents _jwtBearerEvents = new()
    {
        OnTokenValidated = context =>
        {
            var claimsIdentity = new ClaimsIdentity(context.Principal!.Identity);

            var clientId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.ClientId)?.Value;
            if (clientId == null)
                return Task.CompletedTask;

            aclOptions.Value.Clients.TryGetValue(clientId, out var client);
            if (client == null)
                return Task.CompletedTask;

            claimsIdentity.AddClaims([
                .. client.Bcps.Select(ClaimTypes.CreateBcpClaim),
                .. client.ChedTypes.Select(ClaimTypes.CreateChedTypeClaim),
            ]);

            context.Principal = new ClaimsPrincipal(claimsIdentity);

            return Task.CompletedTask;
        },
    };

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Setting Events in a constructor seemed to be overriden or ignored when HandleAuthenticateAsync() is run
        Events = _jwtBearerEvents;
        return base.HandleAuthenticateAsync();
    }
}
