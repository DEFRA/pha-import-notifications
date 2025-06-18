using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public sealed class UpdatedImportNotificationRequest : IDateTimeRangeDefinition
{
    private const string DEFAULT_PAGE = "1";

    private const string DEFAULT_PAGESIZE = "100";

    internal const string MAX_PAGESIZE = "1000";

    [Description(
        "Filter import notifications by BCP.  This parameter is optional if the caller is authorised to see all BCPs, mandatory otherwise."
    )]
    [FromQuery(Name = "bcp")]
    public string[]? Bcp { get; init; }

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

    [Description($"Page number (1-based). Defaults to {DEFAULT_PAGE} if not specified.")]
    [FromQuery(Name = "page")]
    public int? PageFromQuery { get; init; }

    internal int Page => PageFromQuery ?? int.Parse(DEFAULT_PAGE);

    [Description($"Number of items per page. Defaults to {DEFAULT_PAGESIZE} if not specified. Max of {MAX_PAGESIZE}.")]
    [FromQuery(Name = "pageSize")]
    public int? PageSizeFromQuery { get; init; }

    internal int PageSize => PageSizeFromQuery ?? int.Parse(DEFAULT_PAGESIZE);
}

public interface IDateTimeRangeDefinition
{
    DateTime From { get; init; }
    DateTime To { get; init; }
}
