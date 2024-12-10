using Defra.PhaImportNotifications.Api.Endpoints.Validation;

namespace Defra.PhaImportNotifications.Api.Tests.Validators;

public class DateTimeRangeFromEarlierThanToValidatorTests
{
    [Fact]
    public void IsValid_ShouldReturnTrue_When_FromLaterThanTo()
    {
        var range = new TimeRangeBuilder()
            .From(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)))
            .To(DateTime.UtcNow)
            .Build();
        new DateTimeRangeFromEarlierThanToValidator().Validate(range).IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_When_ToLaterThanFrom()
    {
        var range = new TimeRangeBuilder()
            .From(DateTime.UtcNow)
            .To(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)))
            .Build();

        var result = new DateTimeRangeFromEarlierThanToValidator().Validate(range);
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorMessage.Should().Be("'from' must be earlier than 'to'.");
    }
}
