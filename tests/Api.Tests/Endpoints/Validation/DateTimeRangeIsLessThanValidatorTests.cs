using Defra.PhaImportNotifications.Api.Endpoints.Validation;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public class DateTimeRangeIsLessThanValidatorTests
{
    public static TheoryData<TimeRange, TimeSpan> DateTimeRangeDoesntExceedTimespan = new()
    {
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(TimeSpan.FromMinutes(29))), TimeSpan.FromMinutes(30) },
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(new TimeSpan(29, 23, 59, 59))), TimeSpan.FromDays(30) },
    };

    [Theory, MemberData(nameof(DateTimeRangeDoesntExceedTimespan))]
    public void ValidatorIsValidShouldReturnTrueWhenRangeDurationIsLessThanSpan(TimeRange range, TimeSpan timespan) =>
        new DateTimeRangeIsLessThanValidator(timespan).Validate(range).IsValid.Should().BeTrue();

    public static TheoryData<TimeRange, TimeSpan> DateTimeRangeExceedTimespan = new()
    {
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(TimeSpan.FromMinutes(31))), TimeSpan.FromMinutes(30) },
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(new TimeSpan(30, 0, 0, 1))), TimeSpan.FromDays(30) },
    };

    [Theory, MemberData(nameof(DateTimeRangeExceedTimespan))]
    public void ValidatorIsValidShouldReturnTrueWhenRangeDurationIsGreaterThanSpan(
        TimeRange range,
        TimeSpan timespan
    ) => new DateTimeRangeIsLessThanValidator(timespan).Validate(range).IsValid.Should().BeFalse();
}
