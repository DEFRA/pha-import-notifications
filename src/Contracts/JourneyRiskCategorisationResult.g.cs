namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class JourneyRiskCategorisationResult
    {
        [JsonPropertyName("riskLevel")]
        public JourneyRiskCategorisationResultRiskLevelEnum RiskLevel { get; init; }

        [JsonPropertyName("riskLevelMethod")]
        public JourneyRiskCategorisationResultRiskLevelMethodEnum RiskLevelMethod { get; init; }

        [JsonPropertyName("riskLevelDateTime")]
        [Description("The date and time the risk level has been set for a notification")]
        public DateTime RiskLevelDateTime { get; init; }
    }
}
