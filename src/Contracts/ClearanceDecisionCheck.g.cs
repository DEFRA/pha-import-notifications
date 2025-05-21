#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ClearanceDecisionCheck
{
    [JsonPropertyName("checkCode")]
    public required string CheckCode { get; init; }

    [JsonPropertyName("decisionCode")]
    public required string DecisionCode { get; init; }

    [JsonPropertyName("decisionsValidUntil")]
    public DateTime? DecisionsValidUntil { get; init; }

    [JsonPropertyName("decisionReasons")]
    public List<string>? DecisionReasons { get; init; }

    [JsonPropertyName("decisionInternalFurtherDetail")]
    public List<string>? DecisionInternalFurtherDetail { get; init; }
}
