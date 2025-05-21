#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Finalisation
{
    [JsonPropertyName("externalCorrelationId")]
    public string? ExternalCorrelationId { get; init; }

    [JsonPropertyName("messageSentAt")]
    public required DateTime MessageSentAt { get; init; }

    [JsonPropertyName("externalVersion")]
    public required int ExternalVersion { get; init; }

    [JsonPropertyName("decisionNumber")]
    public int? DecisionNumber { get; init; }

    [JsonPropertyName("finalState")]
    [ExampleValue("0")]
    [ExampleValue("1")]
    [ExampleValue("2")]
    [ExampleValue("3")]
    [ExampleValue("4")]
    [ExampleValue("5")]
    [ExampleValue("6")]
    [Description("Possible values taken from Finalisation schema version v1.")]
    public required string FinalState { get; init; }

    [JsonPropertyName("isManualRelease")]
    public required bool IsManualRelease { get; init; }
}
