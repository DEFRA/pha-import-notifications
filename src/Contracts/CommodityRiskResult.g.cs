#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CommodityRiskResult
{
    [JsonPropertyName("riskDecision")]
    [ExampleValue("REQUIRED")]
    [ExampleValue("NOTREQUIRED")]
    [ExampleValue("INCONCLUSIVE")]
    [ExampleValue("REENFORCED_CHECK")]
    [Description("CHED-A, CHED-D, CHED-P - what is the commodity complement risk decision. Possible values taken from IPAFFS schema version 17.5.")]
    public string? RiskDecision { get; init; }

    [JsonPropertyName("exitRiskDecision")]
    [ExampleValue("REQUIRED")]
    [ExampleValue("NOTREQUIRED")]
    [ExampleValue("INCONCLUSIVE")]
    [Description("Transit CHED - what is the commodity complement exit risk decision. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ExitRiskDecision { get; init; }

    [JsonPropertyName("hmiDecision")]
    [ExampleValue("REQUIRED")]
    [ExampleValue("NOTREQUIRED")]
    [Description("HMI decision required. Possible values taken from IPAFFS schema version 17.5.")]
    public string? HmiDecision { get; init; }

    [JsonPropertyName("phsiDecision")]
    [ExampleValue("REQUIRED")]
    [ExampleValue("NOTREQUIRED")]
    [Description("PHSI decision required. Possible values taken from IPAFFS schema version 17.5.")]
    public string? PhsiDecision { get; init; }

    [JsonPropertyName("phsiClassification")]
    [ExampleValue("Mandatory")]
    [ExampleValue("Reduced")]
    [ExampleValue("Controlled")]
    [Description("PHSI classification. Possible values taken from IPAFFS schema version 17.5.")]
    public string? PhsiClassification { get; init; }

    [JsonPropertyName("phsi")]
    [Description("PHSI Decision Breakdown")]
    public Phsi? Phsi { get; init; }

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
