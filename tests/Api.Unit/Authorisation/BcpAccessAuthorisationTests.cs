using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Authentication;
using Defra.PhaImportNotifications.Api.Extensions;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Authorisation;

public class BcpAccessAuthorisationExtensionsTests
{
    [Fact]
    public void ClientHasAccessTo_WhenClientHasClaimToBcp_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        claimsPrincipal.ClientHasAccessTo(["bcp1"]).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToBcp_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        claimsPrincipal.ClientHasAccessTo(["bcp3"]).Should().BeFalse();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToAllRequestedBcps_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1"), new Claim(PhaClaimTypes.Bcp, "bcp2")], "test")
        );

        claimsPrincipal.ClientHasAccessTo(["bcp1", "bcp2", "bcp3"]).Should().BeFalse();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientHasClaimForAllBcps_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "*")], "test"));

        claimsPrincipal.ClientHasAccessTo(["bcp1", "bcp2"]).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenNoBcpsProvided_AndClientHasClaimForAllBcps_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "*")], "test"));

        claimsPrincipal.ClientHasAccessTo([]).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenNoBcpsProvided_AndClientDoesNotHaveClaimForAllBcps_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([new Claim(PhaClaimTypes.Bcp, "bcp1")], "test"));

        claimsPrincipal.ClientHasAccessTo([]).Should().BeFalse();
    }
}
