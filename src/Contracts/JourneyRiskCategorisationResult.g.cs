#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JourneyRiskCategorisationResult
{
    [JsonPropertyName("riskLevel")]
    [Description("Risk Level is defined using enum values High,Medium,Low")]
    public JourneyRiskCategorisationResultRiskLevelEnum? RiskLevel { get; init; }

    [JsonPropertyName("riskLevelMethod")]
    [Description("Indicator of whether the risk level was determined by the system or by the user")]
    public JourneyRiskCategorisationResultRiskLevelMethodEnum? RiskLevelMethod { get; init; }

    [JsonPropertyName("riskLevelSetFor")]
    [Description("The date and time the risk level has been set for a notification")]
    public DateTime? RiskLevelSetFor { get; init; }
}
