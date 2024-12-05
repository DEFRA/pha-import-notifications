using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

internal sealed class UpdatedImportNotificationRequest
{
    [Description("Filter import notifications updated after this date and time. Format is ISO 8601-1:2019")]
    [FromQuery(Name = "from")]
    public DateTime From { get; init; }

    [Description(
        "Filter import notifications updated before this date and time. Format is ISO 8601-1:2019. Default is now ie. "
            + "time of request. If the time period between from and to is greater than 24 hours then "
            + "the request will be invalid."
    )]
    [FromQuery(Name = "to")]
    public DateTime? To { get; init; } = DateTime.Now;
}
