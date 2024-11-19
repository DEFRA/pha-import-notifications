namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class MeansOfTransport
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("document")]
        [Description("Document for transport")]
        public string Document { get; set; }

        [JsonPropertyName("id")]
        [Description("ID of transport")]
        public string Id { get; set; }
    }
}