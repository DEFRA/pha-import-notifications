using Defra.PhaImportNotifications.Api.Endpoints.Validation;
using FluentValidation.TestHelper;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public class ChedReferenceNumberValidatorTests
{
    private ChedReferenceNumberValidator Subject { get; } = new();

    [Theory]
    [InlineData("12345")]
    [InlineData("cheda.GB.2024.1234567")]
    public void Validate_WhenInvalid_ShouldError(string chedReferenceNumber)
    {
        var result = Subject.TestValidate(chedReferenceNumber);

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
