namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class RiskAssessmentResult
    {
        [JsonPropertyName("commodityResults")]
        [Description("List of risk assessed commodities")]
        public Array CommodityResults { get; init; }

        [JsonPropertyName("assessedOn")]
        [Description("Date and time of assessment")]
        public DateTime AssessedOn { get; init; }
    }
}
