using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class DateTimeRangeIsLessThanValidator : AbstractValidator<IDateTimeRangeDefinition>
{
    public DateTimeRangeIsLessThanValidator(TimeSpan timeSpan)
    {
        RuleFor(x => (x.From - x.To).Duration())
            .LessThan(timeSpan)
            .WithName("To")
            .WithMessage($"Must be within {timeSpan.Duration().TotalSeconds} seconds of From");
    }
}
