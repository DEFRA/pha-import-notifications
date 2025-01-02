using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;
using Defra.PhaImportNotifications.Api.Authorisation;

namespace Defra.PhaImportNotifications.Api.Tests.Authorisation;

public class BcpAccessAuthorisationTests
{
    [Fact]
    public void ClientHasAccessTo_WhenClientHasClaimToBcp_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        BcpAccessAuthorisation.ClientHasAccessTo(claimsPrincipal, ["bcp1"]).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToBcp_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        BcpAccessAuthorisation.ClientHasAccessTo(claimsPrincipal, ["bcp3"]).Should().BeFalse();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToAllBcps_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        BcpAccessAuthorisation.ClientHasAccessTo(claimsPrincipal, ["bcp1", "bcp2", "bcp3"]).Should().BeFalse();
    }
}
