using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using FluentValidation;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public class UpdatedImportNotificationRequestValidator : ValidationEndpointFilter<UpdatedImportNotificationRequest>
{
    public UpdatedImportNotificationRequestValidator()
    {
        Include(new DateTimeRangeFromEarlierThanToValidator());
        Include(new DateTimeRangeIsLessThanValidator(TimeSpan.FromHours(1)));
        RuleFor(x => x.To)
            .SetValidator(new DateTimeEarlierThanValidator(TimeSpan.FromSeconds(30), DateTime.UtcNow))
            .WithMessage("To must be older the 30 seconds ago from now");
    }

    protected override int ArgumentIndex => 0;
}
