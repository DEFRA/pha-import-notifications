using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;

namespace Defra.PhaImportNotifications.Api.Extensions;

public static class BcpAccessAuthorisationExtensions
{
    public static bool ClientHasAccessTo(this ClaimsPrincipal user, List<string> bcps)
    {
        var bcpClaims = user.Claims.Where(c => c.Type == PhaClaimTypes.Bcp).Select(c => c.Value);

        return bcps.All(bcp => bcpClaims.Contains(bcp));
    }
}
