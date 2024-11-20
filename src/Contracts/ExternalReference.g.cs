namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ExternalReference
    {
        [JsonPropertyName("system")]
        public ExternalReferenceSystemEnum System { get; init; }

        [JsonPropertyName("reference")]
        [Description("Reference which is added to the notification when either sent to the downstream system or received from it")]
        public string Reference { get; init; }

        [JsonPropertyName("exactMatch")]
        [Description("Details whether there's an exact match between the external source and IPAFFS data")]
        public bool ExactMatch { get; init; }

        [JsonPropertyName("verifiedByImporter")]
        [Description("Details whether an importer has verified the data from an external source")]
        public bool VerifiedByImporter { get; init; }

        [JsonPropertyName("verifiedByInspector")]
        [Description("Details whether an inspector has verified the data from an external source")]
        public bool VerifiedByInspector { get; init; }
    }
}