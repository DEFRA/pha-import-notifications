using System.ComponentModel;

namespace Defra.PhaImportNotifications.Api.Endpoints;

internal sealed record PagedResponse<T>
{
    [Description("Records for current page")]
    public IEnumerable<T> Records { get; init; } = [];

    [Description("Total records across all pages")]
    public int TotalRecords { get; init; }

    [Description("Current page")]
    public int CurrentPage { get; init; }

    [Description("Total number of pages")]
    public int TotalPages { get; init; }
}
