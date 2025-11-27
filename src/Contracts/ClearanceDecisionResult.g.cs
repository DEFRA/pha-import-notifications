#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ClearanceDecisionResult
{
    [JsonPropertyName("itemNumber")]
    public required int ItemNumber { get; init; }

    [JsonPropertyName("importPreNotification")]
    public string? ImportPreNotification { get; init; }

    [JsonPropertyName("documentReference")]
    public string? DocumentReference { get; init; }

    [JsonPropertyName("documentCode")]
    public string? DocumentCode { get; init; }

    [JsonPropertyName("checkCode")]
    public string? CheckCode { get; init; }

    [JsonPropertyName("decisionCode")]
    public string? DecisionCode { get; init; }

    [JsonPropertyName("decisionReason")]
    public string? DecisionReason { get; init; }

    [JsonPropertyName("internalDecisionCode")]
    public string? InternalDecisionCode { get; init; }
}
