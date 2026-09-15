using System.Text;
using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = Defra.PhaImportNotifications.Api.Authentication.ClaimTypes;

namespace Defra.PhaImportNotifications.Api.Endpoints.Token;

public static class EndpointRouteBuilderExtensions
{
    private const string ClientCredentials = "client_credentials";

    public static void MapLocalDevTokenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("oauth2/token", CreateToken).WithName("LocalDevToken").ExcludeFromDescription();
    }

    [HttpPost]
    private static async Task<IResult> CreateToken(
        HttpRequest request,
        [FromServices] IOptions<AclOptions> acl,
        [FromServices] IOptions<LocalDevTokenOptions> options,
        CancellationToken cancellationToken
    )
    {
        var form = request.HasFormContentType ? await request.ReadFormAsync(cancellationToken) : null;

        string? Value(string name) => form?[name].FirstOrDefault() ?? request.Query[name].FirstOrDefault();

        var grantType = Value("grant_type") ?? ClientCredentials;
        if (grantType != ClientCredentials)
            return Results.Problem(
                $"Unsupported grant_type '{grantType}'",
                statusCode: StatusCodes.Status400BadRequest
            );

        var clientId = Value("client_id");
        if (string.IsNullOrWhiteSpace(clientId))
            return Results.Problem("client_id is required", statusCode: StatusCodes.Status400BadRequest);

        if (!acl.Value.Clients.ContainsKey(clientId))
            return Results.Problem(
                $"'{clientId}' is not configured under Acl:Clients",
                statusCode: StatusCodes.Status400BadRequest
            );

        var lifetime = TimeSpan.FromMinutes(options.Value.LifetimeMinutes);
        var now = DateTime.UtcNow;

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = options.Value.Issuer,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.Add(lifetime),
            Claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = clientId,
                [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
                [ClaimTypes.ClientId] = clientId,
                ["token_use"] = "access",
                ["scope"] = "pha-import-notifications-resource-srv/access",
                ["version"] = 2,
            },
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256
            ),
        };

        return Results.Ok(
            new TokenResponse
            {
                AccessToken = new JsonWebTokenHandler().CreateToken(descriptor),
                ExpiresIn = (int)lifetime.TotalSeconds,
            }
        );
    }
}
