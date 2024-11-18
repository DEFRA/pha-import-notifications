namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Phsi
    {
        [JsonPropertyName("documentCheck")]
        [Description("Whether or not a documentary check is required for PHSI")]
        public bool DocumentCheck { get; set; }

        [JsonPropertyName("identityCheck")]
        [Description("Whether or not an identity check is required for PHSI")]
        public bool IdentityCheck { get; set; }

        [JsonPropertyName("physicalCheck")]
        [Description("Whether or not a physical check is required for PHSI")]
        public bool PhysicalCheck { get; set; }
    }
}
