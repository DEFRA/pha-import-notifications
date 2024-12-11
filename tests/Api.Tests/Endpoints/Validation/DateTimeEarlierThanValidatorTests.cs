using Defra.PhaImportNotifications.Api.Endpoints.Validation;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public class DateTimeEarlierThanValidatorTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_When_FromLaterThanTo()
    {
        new DateTimeEarlierThanValidator(TimeSpan.FromSeconds(1), DateTime.Now)
            .Validate(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(2)))
            .IsValid.Should()
            .BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_When_LaterThanExpected()
    {
        var result = new DateTimeEarlierThanValidator(TimeSpan.FromSeconds(1), DateTime.UtcNow).Validate(
            DateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds(500))
        );
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
    }
}
