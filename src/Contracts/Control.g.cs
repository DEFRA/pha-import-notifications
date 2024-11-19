namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Control
    {
        [JsonPropertyName("feedbackInformation")]
        public FeedbackInformation FeedbackInformation { get; set; }

        [JsonPropertyName("detailsOnReExport")]
        public DetailsOnReExport DetailsOnReExport { get; set; }

        [JsonPropertyName("officialInspector")]
        public OfficialInspector OfficialInspector { get; set; }

        [JsonPropertyName("consignmentLeave")]
        public int ConsignmentLeave { get; set; }
    }
}
