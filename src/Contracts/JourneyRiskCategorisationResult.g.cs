#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record JourneyRiskCategorisationResult
{
    [JsonPropertyName("riskLevel")]
    [ExampleValue("High")]
    [ExampleValue("Medium")]
    [ExampleValue("Low")]
    [Description("Risk Level is defined using enum values High,Medium,Low. Possible values taken from IPAFFS schema version 17.5.")]
    public string? RiskLevel { get; init; }

    [JsonPropertyName("riskLevelMethod")]
    [ExampleValue("System")]
    [ExampleValue("User")]
    [Description("Indicator of whether the risk level was determined by the system or by the user. Possible values taken from IPAFFS schema version 17.5.")]
    public string? RiskLevelMethod { get; init; }

    [JsonPropertyName("riskLevelSetFor")]
    [Description("The date and time the risk level has been set for a notification")]
    public DateTime? RiskLevelSetFor { get; init; }
}
