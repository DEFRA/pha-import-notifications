using System.Security.Claims;

namespace Defra.PhaImportNotifications.Api.Authentication;

public static class ClaimTypes
{
    public const string Bcp = "Bcp";
    public const string ChedType = "ChedType";
    public const string ClientId = "client_id";

    public static Claim CreateBcpClaim(string bcp) => new(Bcp, bcp);

    public static Claim CreateChedTypeClaim(string chedType) => new(ChedType, chedType);
}
