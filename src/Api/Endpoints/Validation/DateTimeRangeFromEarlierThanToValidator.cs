using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class DateTimeRangeFromEarlierThanToValidator : AbstractValidator<IDateTimeRangeDefinition>
{
    public DateTimeRangeFromEarlierThanToValidator()
    {
        RuleFor(x => x.From).LessThan(x => x.To).WithMessage("Must be earlier than To");
    }
}
