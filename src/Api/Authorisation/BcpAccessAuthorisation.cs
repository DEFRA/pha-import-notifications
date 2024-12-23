using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;

namespace Defra.PhaImportNotifications.Api.Authorisation;

public static class BcpAccessAuthorisation
{
    public static bool ClientHasAccessTo(ClaimsPrincipal user, List<string> bcps)
    {
        var bcpClaims = user.Claims.Where(c => c.Type == PhaClaimTypes.Bcp).Select(c => c.Value);

        return bcps.All(bcp => bcpClaims.Contains(bcp));
    }
}
