using Defra.PhaImportNotifications.Api.Endpoints.Validation;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public class DateTimeRangeIsLessThanOrEqualToValidatorTests
{
    public static TheoryData<TimeRange, TimeSpan> DateTimeRangeDoesntExceedTimespan = new()
    {
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(TimeSpan.FromMinutes(29))), TimeSpan.FromMinutes(30) },
        { new TimeRange(DateTime.UtcNow, DateTime.UtcNow.Add(TimeSpan.FromMinutes(30))), TimeSpan.FromMinutes(30) },
    };

    [Theory, MemberData(nameof(DateTimeRangeDoesntExceedTimespan))]
    public void Validate_IsValid_WhenRangeDurationIsLessOrEqualToTimeSpan(TimeRange range, TimeSpan timeSpan) =>
        new DateTimeRangeIsLessThanOrEqualToValidator(timeSpan).Validate(range).IsValid.Should().BeTrue();

    public static TheoryData<TimeRange, TimeSpan> DateTimeRangeExceedTimespan = new()
    {
        {
            new TimeRange(
                DateTime.UtcNow,
                DateTime.UtcNow.Add(TimeSpan.FromMinutes(30).Add(TimeSpan.FromMilliseconds(1)))
            ),
            TimeSpan.FromMinutes(30)
        },
    };

    [Theory, MemberData(nameof(DateTimeRangeExceedTimespan))]
    public void Validate_IsInvalid_WhenRangeDurationIsGreaterThanTimeSpan(TimeRange range, TimeSpan timespan) =>
        new DateTimeRangeIsLessThanOrEqualToValidator(timespan).Validate(range).IsValid.Should().BeFalse();
}
