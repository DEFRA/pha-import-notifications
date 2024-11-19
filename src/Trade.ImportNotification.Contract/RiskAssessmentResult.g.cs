namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class RiskAssessmentResult
    {
        [JsonPropertyName("commodityResults")]
        [Description("List of risk assessed commodities")]
        public Array CommodityResults { get; set; }

        [JsonPropertyName("assessedOn")]
        [Description("Date and time of assessment")]
        public string AssessedOn { get; set; }
    }
}
