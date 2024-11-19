namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class JourneyRiskCategorisationResult
    {
        [JsonPropertyName("riskLevel")]
        [Description("")]
        public int RiskLevel { get; set; }

        [JsonPropertyName("riskLevelMethod")]
        [Description("")]
        public int RiskLevelMethod { get; set; }

        [JsonPropertyName("riskLevelDateTime")]
        [Description("The date and time the risk level has been set for a notification")]
        public string RiskLevelDateTime { get; set; }
    }
}
