namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Control
    {
        [JsonPropertyName("feedbackInformation")]
        public FeedbackInformation FeedbackInformation { get; init; }

        [JsonPropertyName("detailsOnReExport")]
        public DetailsOnReExport DetailsOnReExport { get; init; }

        [JsonPropertyName("officialInspector")]
        public OfficialInspector OfficialInspector { get; init; }

        [JsonPropertyName("consignmentLeave")]
        public int ConsignmentLeave { get; init; }
    }
}
