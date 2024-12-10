using Defra.PhaImportNotifications.Api.Helpers;
using FluentAssertions;

namespace Defra.PhaImportNotifications.Api.Tests.Helpers;

public class BasicAuthHelperTests
{
    private readonly string BasicAuthValue = "dXNlcm5hbWU6cGFzc3dvcmQ="; // username: password
    private readonly string Passsword = "password";
    private readonly string Username = "username";

    [Fact]
    public void CreateBasicAuthHeader_ReturnsTheCorrectAuthHeader()
    {
        var result = BasicAuthHelper.CreateBasicAuthHeader(Username, Passsword);
        result.Should().Be($"Basic {BasicAuthValue}");
    }

    [Fact]
    public void CreateBasicAuth_ReturnsTheCorrectValue()
    {
        var result = BasicAuthHelper.CreateBasicAuth(Username, Passsword);
        result.Should().Be(BasicAuthValue);
    }

    [Fact]
    public void FromBasicAuth_ReturnsTheCorrectUsernameAndPassword()
    {
        var result = BasicAuthHelper.FromBasicAuth(BasicAuthValue);
        result.Should().Be((Username, Passsword));
    }
}
