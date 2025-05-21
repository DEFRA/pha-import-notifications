#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ClearanceDecision
{
    [JsonPropertyName("externalCorrelationId")]
    public string? ExternalCorrelationId { get; init; }

    [JsonPropertyName("timestamp")]
    public required DateTime Timestamp { get; init; }

    [JsonPropertyName("externalVersionNumber")]
    public int? ExternalVersionNumber { get; init; }

    [JsonPropertyName("decisionNumber")]
    public int? DecisionNumber { get; init; }

    [JsonPropertyName("sourceVersion")]
    public string? SourceVersion { get; init; }

    [JsonPropertyName("items")]
    public required List<ClearanceDecisionItem> Items { get; init; }
}
