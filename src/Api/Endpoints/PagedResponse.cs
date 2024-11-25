using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints;

internal sealed class PagedResponse<T>
{
    [Description("Records for current page")]
    [Required]
    public IEnumerable<T> Records { get; init; } = [];

    [Description("Total records across all pages")]
    [Required]
    public int TotalRecords { get; init; }

    [Description("Current page")]
    [Required]
    public int CurrentPage { get; init; }

    [Description("Total number of pages")]
    [Required]
    public int TotalPages { get; init; }
}
