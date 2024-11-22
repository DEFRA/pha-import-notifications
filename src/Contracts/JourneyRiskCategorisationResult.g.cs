using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JourneyRiskCategorisationResult
{
    [JsonPropertyName("riskLevel")]
    public required JourneyRiskCategorisationResultRiskLevelEnum RiskLevel { get; init; }

    [JsonPropertyName("riskLevelMethod")]
    public required JourneyRiskCategorisationResultRiskLevelMethodEnum RiskLevelMethod { get; init; }

    [JsonPropertyName("riskLevelDateTime")]
    [Description("The date and time the risk level has been set for a notification")]
    public DateTime? RiskLevelDateTime { get; init; }
}
