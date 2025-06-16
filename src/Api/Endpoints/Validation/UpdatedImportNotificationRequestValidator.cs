using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class UpdatedImportNotificationRequestValidator : ValidationEndpointFilter<UpdatedImportNotificationRequest>
{
    public UpdatedImportNotificationRequestValidator()
    {
        RuleFor(x => x.From).Must(x => x.Kind == DateTimeKind.Utc).WithMessage("Must be UTC");

        RuleFor(x => x.To).Must(x => x.Kind == DateTimeKind.Utc).WithMessage("Must be UTC");

        Include(new DateTimeRangeFromEarlierThanToValidator());

        Include(new DateTimeRangeIsLessThanOrEqualToValidator(TimeSpan.FromHours(1)));

        RuleFor(x => x.To)
            .Must(x => x < DateTime.UtcNow.AddSeconds(-30))
            .WithMessage("Must be more than 30 seconds before UTC now");

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage($"Must be greater than or equal to 1");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, int.Parse(UpdatedImportNotificationRequest.MAX_PAGESIZE))
            .WithMessage($"Must be between 1 and {UpdatedImportNotificationRequest.MAX_PAGESIZE} (inclusive)");
    }

    protected override int ArgumentIndex => 0;
}
