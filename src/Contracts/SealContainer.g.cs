namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SealContainer
    {
        [JsonPropertyName("sealNumber")]
        public string SealNumber { get; init; }

        [JsonPropertyName("containerNumber")]
        public string ContainerNumber { get; init; }

        [JsonPropertyName("officialSeal")]
        public bool OfficialSeal { get; init; }

        [JsonPropertyName("resealedSealNumber")]
        public string ResealedSealNumber { get; init; }
    }
}
