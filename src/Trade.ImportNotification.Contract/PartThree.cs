namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartThree
    {
        [JsonPropertyName("controlStatus")]
        [Description("")]
        public int ControlStatus { get; set; }

        [JsonPropertyName("control")]
        [Description("")]
        public Control Control { get; set; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for Part 3 - Control")]
        public Array ConsignmentValidations { get; set; }

        [JsonPropertyName("sealCheckRequired")]
        [Description("Is the seal check required")]
        public bool SealCheckRequired { get; set; }

        [JsonPropertyName("sealCheck")]
        [Description("")]
        public SealCheck SealCheck { get; set; }

        [JsonPropertyName("sealCheckOverride")]
        [Description("")]
        public InspectionOverride SealCheckOverride { get; set; }
    }
}