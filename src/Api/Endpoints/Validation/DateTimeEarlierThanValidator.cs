using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class DateTimeEarlierThanValidator : AbstractValidator<DateTime>
{
    public DateTimeEarlierThanValidator(TimeSpan timeSpan, DateTime dateTime)
    {
        RuleFor(x => x).LessThan(_ => dateTime.Subtract(timeSpan));
    }
}
