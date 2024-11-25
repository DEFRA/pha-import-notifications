#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class CommodityRiskResult
{
    [JsonPropertyName("riskDecision")]
    public CommodityRiskResultRiskDecisionEnum RiskDecision { get; set; }

    [JsonPropertyName("exitRiskDecision")]
    public CommodityRiskResultExitRiskDecisionEnum ExitRiskDecision { get; set; }

    [JsonPropertyName("hmiDecision")]
    public CommodityRiskResultHmiDecisionEnum HmiDecision { get; set; }

    [JsonPropertyName("phsiDecision")]
    public CommodityRiskResultPhsiDecisionEnum PhsiDecision { get; set; }

    [JsonPropertyName("phsiClassification")]
    public CommodityRiskResultPhsiClassificationEnum PhsiClassification { get; set; }

    [JsonPropertyName("phsi")]
    public Phsi Phsi { get; set; }

    [JsonPropertyName("uniqueId")]
    [Description("UUID used to match to the complement parameter set")]
    public string? UniqueId { get; set; }

    [JsonPropertyName("eppoCode")]
    [Description("EPPO Code for the species")]
    public string? EppoCode { get; set; }

    [JsonPropertyName("variety")]
    [Description("Name or ID of the variety")]
    public string? Variety { get; set; }

    [JsonPropertyName("isWoody")]
    [Description("Whether or not a plant is woody")]
    public bool? IsWoody { get; set; }

    [JsonPropertyName("indoorOutdoor")]
    [Description("Indoor or Outdoor for a plant")]
    public string? IndoorOutdoor { get; set; }

    [JsonPropertyName("propagation")]
    [Description("Whether the propagation is considered a Plant, Bulb, Seed or None")]
    public string? Propagation { get; set; }

    [JsonPropertyName("phsiRuleType")]
    [Description("Rule type for PHSI checks")]
    public string? PhsiRuleType { get; set; }
}
