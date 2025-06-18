using System.Security.Claims;
using Defra.PhaImportNotifications.Api.Extensions;
using ClaimTypes = Defra.PhaImportNotifications.Api.Authentication.ClaimTypes;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Authorisation;

public class BcpAccessAuthorisationExtensionsTests
{
    private class ClaimsPrincipalBuilder
    {
        private const string AnyIdentifier = "*";

        private readonly List<string> _bcps = [];
        private readonly List<string> _chedTypes = [];

        public ClaimsPrincipalBuilder WithBcps(params string[] bcps)
        {
            _bcps.AddRange(bcps);
            return this;
        }

        public ClaimsPrincipalBuilder WithAnyBcps() => WithBcps(AnyIdentifier);

        public ClaimsPrincipalBuilder WithChedTypes(params string[] chedTypes)
        {
            _chedTypes.AddRange(chedTypes);
            return this;
        }

        public ClaimsPrincipalBuilder WithAnyChedType() => WithChedTypes(AnyIdentifier);

        public ClaimsPrincipal Build() =>
            new(
                new ClaimsIdentity(
                    [.. _bcps.Select(ClaimTypes.CreateBcpClaim), .. _chedTypes.Select(ClaimTypes.CreateChedTypeClaim)],
                    "test"
                )
            );
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientHasClaimToBcp_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithBcps("bcp1", "bcp2").WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo(["bcp1"], []).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToBcp_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithBcps("bcp1", "bcp2").WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo(["bcp3"], []).Should().BeFalse();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientDoesNotHaveClaimToAllRequestedBcps_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithBcps("bcp1", "bcp2").WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo(["bcp1", "bcp2", "bcp3"], []).Should().BeFalse();
    }

    [Fact]
    public void ClientHasAccessTo_WhenClientHasClaimForAllBcps_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithAnyBcps().WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo(["bcp1", "bcp2"], []).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenNoBcpsProvided_AndClientHasClaimForAllBcps_ReturnsTrue()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithAnyBcps().WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo([], []).Should().BeTrue();
    }

    [Fact]
    public void ClientHasAccessTo_WhenNoBcpsProvided_AndClientDoesNotHaveClaimForAllBcps_ReturnsFalse()
    {
        var claimsPrincipal = new ClaimsPrincipalBuilder().WithBcps("bcp1").WithAnyChedType().Build();

        claimsPrincipal.ClientHasAccessTo([], []).Should().BeFalse();
    }
}
