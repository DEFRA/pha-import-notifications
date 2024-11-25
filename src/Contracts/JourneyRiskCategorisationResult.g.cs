#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JourneyRiskCategorisationResult
{
    [JsonPropertyName("riskLevel")]
    public JourneyRiskCategorisationResultRiskLevelEnum RiskLevel { get; set; }

    [JsonPropertyName("riskLevelMethod")]
    public JourneyRiskCategorisationResultRiskLevelMethodEnum RiskLevelMethod { get; set; }

    [JsonPropertyName("riskLevelDateTime")]
    [Description("The date and time the risk level has been set for a notification")]
    public DateTime? RiskLevelDateTime { get; set; }
}
