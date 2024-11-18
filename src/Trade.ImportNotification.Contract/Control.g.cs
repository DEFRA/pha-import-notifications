namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Control
    {
        [JsonPropertyName("feedbackInformation")]
        [Description("")]
        public FeedbackInformation FeedbackInformation { get; set; }

        [JsonPropertyName("detailsOnReExport")]
        [Description("")]
        public DetailsOnReExport DetailsOnReExport { get; set; }

        [JsonPropertyName("officialInspector")]
        [Description("")]
        public OfficialInspector OfficialInspector { get; set; }

        [JsonPropertyName("consignmentLeave")]
        [Description("")]
        public int ConsignmentLeave { get; set; }
    }
}
