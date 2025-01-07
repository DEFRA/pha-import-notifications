using Defra.PhaImportNotifications.Api.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Defra.PhaImportNotifications.Api.Extensions;

public static class PhaJwtAuthenticationExtensions
{
    public static void AddPhaJwtAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddScheme<JwtBearerOptions, PhaJwtAuthenticationHandler>(
                "Bearer",
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        SignatureValidator = (token, parameters) => new JsonWebToken(token),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                }
            );
        services.AddAuthorization();
    }
}
