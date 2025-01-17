#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record AuditEntry
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("version")]
    public int? Version { get; init; }

    [JsonPropertyName("createdBy")]
    public required CreatedBySystem CreatedBy { get; init; }

    [JsonPropertyName("createdSource")]
    public DateTime? CreatedSource { get; init; }

    [JsonPropertyName("createdLocal")]
    public required DateTime CreatedLocal { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("diff")]
    public List<AuditDiffEntry>? Diff { get; init; }

    [JsonPropertyName("context")]
    public DecisionContext? Context { get; init; }
}
