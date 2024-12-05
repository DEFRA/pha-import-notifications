using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

internal sealed class UpdatedImportNotification
{
    [Description("Date last updated. Format is ISO 8601-1:2019")]
    [Required]
    public required DateTime Updated { get; init; }

    [Description("CHED Reference Number")]
    [RegularExpression($"^{Regexes.ChedReferenceNumber}$")]
    [Required]
    public required string ReferenceNumber { get; init; }

    [Description("Links")]
    [Required]
    public required UpdatedImportNotificationLinks Links { get; init; }
}