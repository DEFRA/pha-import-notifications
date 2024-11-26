using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints;

internal sealed class UpdatedImportNotification
{
    [Description("Last updated date. Format is ISO 8601-1:2019")]
    [Required]
    public required DateTime LastUpdated { get; init; }

    /// <example>/import-notifications/CHEDA.GB.2024.1020304</example>
    [Description("Relative path to import notification")]
    [RegularExpression($"^/import-notifications/{Regexes.ChedReferenceNumber}$")]
    [Required]
    public required string Uri { get; init; }
}
