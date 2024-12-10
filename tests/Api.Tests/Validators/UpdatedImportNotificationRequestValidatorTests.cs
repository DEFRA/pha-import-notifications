using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Endpoints.Validation;

namespace Defra.PhaImportNotifications.Api.Tests.Validators;

public class UpdatedImportNotificationRequestValidatorTests
{
    [Fact]
    public void IsValid_IsTrue_When_RequestIsValid()
    {
        var from = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(60));
        var to = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));

        var request = new UpdatedImportNotificationRequest
        {
            From = from,
            To = to,
            Bcp = ["bcp1"],
        };
        var result = new UpdatedImportNotificationRequestValidator().Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_IsTrue_When_RequestIsInValid()
    {
        var from = DateTime.UtcNow.Add(TimeSpan.FromHours(2));
        var to = DateTime.UtcNow;

        var request = new UpdatedImportNotificationRequest
        {
            From = from,
            To = to,
            Bcp = ["bcp1"],
        };
        var result = new UpdatedImportNotificationRequestValidator().Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(3);
    }
}
