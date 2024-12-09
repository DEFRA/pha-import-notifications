using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

internal sealed class UpdatedImportNotificationLinks
{
    /// <example>/import-notifications/CHEDA.GB.2024.1020304</example>
    [Description("Relative path to import notification")]
    [RegularExpression($"^/import-notifications/{Regexes.ChedReferenceNumber}$")]
    [Required]
    public required Uri ImportNotification { get; init; }
}
