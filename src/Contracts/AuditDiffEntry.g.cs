#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class AuditDiffEntry
{
    [JsonPropertyName("path")]
    public string? Path { get; init; }

    [JsonPropertyName("op")]
    public string? Op { get; init; }

    [JsonPropertyName("value")]
    public object? Value { get; init; }
}
