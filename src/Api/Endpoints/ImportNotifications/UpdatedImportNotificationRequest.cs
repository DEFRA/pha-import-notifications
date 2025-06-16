using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public sealed class UpdatedImportNotificationRequest : IDateTimeRangeDefinition
{
    [Description(
        "Filter import notifications by BCP.  This parameter is optional if the caller is authorised to see all BCPs, mandatory otherwise."
    )]
    [FromQuery(Name = "bcp")]
    public string[]? Bcp { get; init; } = [];

    [Description(
        "Filter import notifications by CHED Type.  This parameter is optional if the caller is authorised to see all CHED Types, mandatory otherwise."
    )]
    [FromQuery(Name = "chedType")]
    public string[]? ChedType { get; init; } = [];

    [Description(
        "Filter import notifications updated at this date and time or after this date and time. "
            + " Expected value is UTC using format ISO 8601-1:2019"
    )]
    [FromQuery(Name = "from")]
    public DateTime From { get; init; }

    [Description(
        "Filter import notifications updated before this date and time. Expected value is UTC using format "
            + "ISO 8601-1:2019. If the time period between From and To is greater than 1 hour then the request will "
            + "be invalid. Expected value also needs to be in the past by 30 seconds if requesting the"
            + " current UTC date and time."
    )]
    [FromQuery(Name = "to")]
    public DateTime To { get; init; }
}

public interface IDateTimeRangeDefinition
{
    DateTime From { get; init; }
    DateTime To { get; init; }
}
