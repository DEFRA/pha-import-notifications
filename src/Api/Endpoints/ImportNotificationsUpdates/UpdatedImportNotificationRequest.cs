using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotificationsUpdates;

internal sealed class UpdatedImportNotificationRequest
{
    [Description("Allows a specific page to be requested")]
    [DefaultValue(1)]
    [FromQuery(Name = "page")]
    public int? Page { get; init; } = 1;

    [Description("Allows a page size to be requested")]
    [DefaultValue(100)]
    [FromQuery(Name = "pageSize")]
    public int? PageSize { get; init; } = 100;

    [Description("Filter import notifications updated after this date and time. Format is ISO 8601-1:2019")]
    [FromQuery(Name = "from")]
    public DateTime From { get; init; }

    [Description(
        "Filter import notifications updated before this date and time. Format is ISO 8601-1:2019. Default is now ie. "
            + "time of request. If the time period between from and to is greater than 24 hours then "
            + "the request will be invalid."
    )]
    [DefaultValue(typeof(DateTime), "Time of execution")] // This doesn't add anything to the spec - do we need it?
    [FromQuery(Name = "to")]
    public DateTime? To { get; init; } = DateTime.Now;
}
