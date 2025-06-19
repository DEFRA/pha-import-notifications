using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public class PagingMetadata
{
    [Description("The total number of items across all pages")]
    [Required]
    public int Total { get; init; }

    [Description("The page number (1-based) in this response")]
    [Required]
    public int Page { get; init; }

    [Description("The maximum number of items per page")]
    [Required]
    public int PageSize { get; init; }
}
