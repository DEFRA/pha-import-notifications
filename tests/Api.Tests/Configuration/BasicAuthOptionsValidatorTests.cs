using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.Tests.Configuration;

public class BasicAuthOptionsValidatorTests
{
    private readonly BasicAuthOptionsValidator _validator = new();

    [Fact]
    public void IfNotEnabled_ValidatesSuccessfully()
    {
        var options = new BasicAuthOptions { Enabled = false };
        _validator.Validate("", options).Should().Be(ValidateOptionsResult.Success);
    }

    [Theory]
    [InlineData("username", "", true)]
    [InlineData("", "password", true)]
    [InlineData("username", "password", false)]
    public void IfEnabled_TheUsernameAndPasswordMustBePresent_OrItFails(string username, string password, bool failed)
    {
        var options = new BasicAuthOptions
        {
            Enabled = true,
            Password = password,
            Username = username,
        };
        _validator.Validate("", options).Failed.Should().Be(failed);
    }
}
