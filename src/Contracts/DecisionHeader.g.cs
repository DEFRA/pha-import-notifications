#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DecisionHeader
{
    [JsonPropertyName("entryReference")]
    public string? EntryReference { get; init; }

    [JsonPropertyName("entryVersionNumber")]
    public int? EntryVersionNumber { get; init; }

    [JsonPropertyName("decisionNumber")]
    public int? DecisionNumber { get; init; }
}
