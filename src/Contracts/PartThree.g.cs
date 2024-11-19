namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartThree
    {
        [JsonPropertyName("controlStatus")]
        public int ControlStatus { get; set; }

        [JsonPropertyName("control")]
        public Control Control { get; set; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for Part 3 - Control")]
        public Array ConsignmentValidations { get; set; }

        [JsonPropertyName("sealCheckRequired")]
        [Description("Is the seal check required")]
        public bool SealCheckRequired { get; set; }

        [JsonPropertyName("sealCheck")]
        public SealCheck SealCheck { get; set; }

        [JsonPropertyName("sealCheckOverride")]
        public InspectionOverride SealCheckOverride { get; set; }
    }
}
