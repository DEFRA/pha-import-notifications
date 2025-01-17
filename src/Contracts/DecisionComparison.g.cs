#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DecisionComparison
{
    [JsonPropertyName("paired")]
    public required bool Paired { get; init; }

    [JsonPropertyName("decisionStatus")]
    public required DecisionStatusEnum DecisionStatus { get; init; }

    [JsonPropertyName("decisionMatched")]
    public required bool DecisionMatched { get; init; }

    [JsonPropertyName("btmsDecisionNumber")]
    public int? BtmsDecisionNumber { get; init; }

    [JsonPropertyName("checks")]
    public List<ItemCheck>? Checks { get; init; }
}
