using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;
using ClaimTypes = Defra.PhaImportNotifications.Api.Authentication.ClaimTypes;

namespace Defra.PhaImportNotifications.Api.Extensions;

public static class BcpAccessAuthorisationExtensions
{
    public static bool ClientHasAccessTo(this ClaimsPrincipal user, List<string> bcps, List<string> chedTypes)
    {
        var claims = GetClaimsByType(user.Claims);

        var hasAccess = (string claimType, List<string> requested) =>
            claims[claimType].Contains("*")
            || (requested.Count != 0 && requested.All(requestedItem => claims[claimType].Contains(requestedItem)));

        return hasAccess(ClaimTypes.Bcp, bcps) && hasAccess(ClaimTypes.ChedType, chedTypes);
    }

    static ILookup<string, string> GetClaimsByType(IEnumerable<Claim> claims) =>
        claims.ToLookup(claim => claim.Type, claim => claim.Value);
}
