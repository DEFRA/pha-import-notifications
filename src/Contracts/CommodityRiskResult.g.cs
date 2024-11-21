namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CommodityRiskResult
    {
        [JsonPropertyName("riskDecision")]
        public CommodityRiskResultRiskDecisionEnum RiskDecision { get; init; }

        [JsonPropertyName("exitRiskDecision")]
        public CommodityRiskResultExitRiskDecisionEnum ExitRiskDecision { get; init; }

        [JsonPropertyName("hmiDecision")]
        public CommodityRiskResultHmiDecisionEnum HmiDecision { get; init; }

        [JsonPropertyName("phsiDecision")]
        public CommodityRiskResultPhsiDecisionEnum PhsiDecision { get; init; }

        [JsonPropertyName("phsiClassification")]
        public CommodityRiskResultPhsiClassificationEnum PhsiClassification { get; init; }

        [JsonPropertyName("phsi")]
        public Phsi Phsi { get; init; }

        [JsonPropertyName("uniqueId")]
        [Description("UUID used to match to the complement parameter set")]
        public string UniqueId { get; init; }

        [JsonPropertyName("eppoCode")]
        [Description("EPPO Code for the species")]
        public string EppoCode { get; init; }

        [JsonPropertyName("variety")]
        [Description("Name or ID of the variety")]
        public string Variety { get; init; }

        [JsonPropertyName("isWoody")]
        [Description("Whether or not a plant is woody")]
        public bool IsWoody { get; init; }

        [JsonPropertyName("indoorOutdoor")]
        [Description("Indoor or Outdoor for a plant")]
        public string IndoorOutdoor { get; init; }

        [JsonPropertyName("propagation")]
        [Description("Whether the propagation is considered a Plant, Bulb, Seed or None")]
        public string Propagation { get; init; }

        [JsonPropertyName("phsiRuleType")]
        [Description("Rule type for PHSI checks")]
        public string PhsiRuleType { get; init; }
    }
}
