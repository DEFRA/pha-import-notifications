#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class AuditEntry
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("createdSource")]
    public DateTime? CreatedSource { get; set; }

    [JsonPropertyName("createdLocal")]
    public DateTime CreatedLocal { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("diff")]
    public List<AuditDiffEntry>? Diff { get; set; }
}
