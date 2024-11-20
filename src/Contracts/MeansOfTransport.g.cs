namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class MeansOfTransport
    {
        [JsonPropertyName("type")]
        public MeansOfTransportTypeEnum Type { get; init; }

        [JsonPropertyName("document")]
        [Description("Document for transport")]
        public string Document { get; init; }

        [JsonPropertyName("id")]
        [Description("ID of transport")]
        public string Id { get; init; }
    }
}
