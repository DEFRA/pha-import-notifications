namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SealContainer
    {
        [JsonPropertyName("sealNumber")]
        [Description("")]
        public string[] SealNumber { get; set; }

        [JsonPropertyName("containerNumber")]
        [Description("")]
        public string[] ContainerNumber { get; set; }

        [JsonPropertyName("officialSeal")]
        [Description("")]
        public bool OfficialSeal { get; set; }

        [JsonPropertyName("resealedSealNumber")]
        [Description("")]
        public string[] ResealedSealNumber { get; set; }
    }
}
