#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class CommodityRiskResult
{
    [JsonPropertyName("riskDecision")]
    public required CommodityRiskResultRiskDecisionEnum RiskDecision { get; init; }

    [JsonPropertyName("exitRiskDecision")]
    public required CommodityRiskResultExitRiskDecisionEnum ExitRiskDecision { get; init; }

    [JsonPropertyName("hmiDecision")]
    public required CommodityRiskResultHmiDecisionEnum HmiDecision { get; init; }

    [JsonPropertyName("phsiDecision")]
    public required CommodityRiskResultPhsiDecisionEnum PhsiDecision { get; init; }

    [JsonPropertyName("phsiClassification")]
    public required CommodityRiskResultPhsiClassificationEnum PhsiClassification { get; init; }

    [JsonPropertyName("phsi")]
    public required Phsi Phsi { get; init; }

    [JsonPropertyName("uniqueId")]
    [Description("UUID used to match to the complement parameter set")]
    public string? UniqueId { get; init; }

    [JsonPropertyName("eppoCode")]
    [Description("EPPO Code for the species")]
    public string? EppoCode { get; init; }

    [JsonPropertyName("variety")]
    [Description("Name or ID of the variety")]
    public string? Variety { get; init; }

    [JsonPropertyName("isWoody")]
    [JsonIgnore]
    [Description("Whether or not a plant is woody")]
    public bool? IsWoody { get; init; }

    [JsonPropertyName("indoorOutdoor")]
    [JsonIgnore]
    [Description("Indoor or Outdoor for a plant")]
    public string? IndoorOutdoor { get; init; }

    [JsonPropertyName("propagation")]
    [Description("Whether the propagation is considered a Plant, Bulb, Seed or None")]
    public string? Propagation { get; init; }

    [JsonPropertyName("phsiRuleType")]
    [Description("Rule type for PHSI checks")]
    public string? PhsiRuleType { get; init; }
}
