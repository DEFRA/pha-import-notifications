using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

internal sealed class UpdatedImportNotificationsResponse
{
    [Description("Import Notifications")]
    [Required]
    public IEnumerable<UpdatedImportNotification> ImportNotifications { get; init; } = [];

    [Description("Metadata related to the paged response and its content")]
    [Required]
    public required PagingMetadata Paging { get; init; }
}
