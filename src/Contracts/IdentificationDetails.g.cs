namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class IdentificationDetails
    {
        [JsonPropertyName("identificationDetail")]
        [Description("Identification detail")]
        public string IdentificationDetail { get; init; }

        [JsonPropertyName("identificationDescription")]
        [Description("Identification description")]
        public string IdentificationDescription { get; init; }
    }
}