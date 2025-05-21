using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;

namespace Defra.PhaImportNotifications.Api.Extensions;

public static class BcpAccessAuthorisationExtensions
{
    public static bool ClientHasAccessTo(this ClaimsPrincipal user, List<string> bcps)
    {
        var bcpClaims = user.Claims.Where(c => c.Type == PhaClaimTypes.Bcp).Select(c => c.Value);

        return (bcpClaims.Contains("*")) || (bcps.Count != 0 && bcps.All(bcp => bcpClaims.Contains(bcp)));
    }
}
