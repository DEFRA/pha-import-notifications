using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class DateTimeRangeIsLessThanOrEqualToValidator : AbstractValidator<IDateTimeRangeDefinition>
{
    public DateTimeRangeIsLessThanOrEqualToValidator(TimeSpan timeSpan)
    {
        RuleFor(x => (x.From - x.To).Duration())
            .LessThanOrEqualTo(timeSpan)
            .WithName("To")
            .WithMessage($"Must not be more than {timeSpan.Duration().TotalSeconds} seconds of From");
    }
}
