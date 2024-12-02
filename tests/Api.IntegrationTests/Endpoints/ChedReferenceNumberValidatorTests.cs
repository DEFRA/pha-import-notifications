using Defra.PhaImportNotifications.Api.Endpoints;
using FluentValidation.TestHelper;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class ChedReferenceNumberValidatorTests
{
    private ChedReferenceNumberValidator Subject { get; } = new();

    [Fact]
    public void Validate_WhenInvalid_ShouldError()
    {
        var result = Subject.TestValidate("12345");

        result.ShouldHaveValidationErrorFor("ChedReferenceNumber");
    }

    [Theory]
    [InlineData("CHEDA.GB.2024.1234567")]
    [InlineData("CHEDD.GB.2024.1234567")]
    [InlineData("CHEDP.GB.2024.1234567")]
    [InlineData("CHEDPP.GB.2024.1234567")]
    public void Validate_WhenValid_ShouldNotError(string chedReferenceNumber)
    {
        var result = Subject.TestValidate(chedReferenceNumber);

        result.ShouldNotHaveValidationErrorFor("ChedReferenceNumber");
    }
}
