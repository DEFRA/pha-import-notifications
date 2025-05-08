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
    [ExampleValue("Btms")]
    [ExampleValue("Ipaffs")]
    [ExampleValue("Alvs")]
    [ExampleValue("Cds")]
    [ExampleValue("Gvms")]
    public required string CreatedBy { get; init; }

    [JsonPropertyName("createdSource")]
    public DateTime? CreatedSource { get; init; }

    [JsonPropertyName("createdLocal")]
    public required DateTime CreatedLocal { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("diff")]
    public List<AuditDiffEntry>? Diff { get; init; }

    [JsonPropertyName("context")]
    public AuditContext? Context { get; init; }
}
