namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartThree
    {
        [JsonPropertyName("controlStatus")]
        public PartThreeControlStatusEnum ControlStatus { get; init; }

        [JsonPropertyName("control")]
        public Control Control { get; init; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for Part 3 - Control")]
        public List<ValidationMessageCode> ConsignmentValidations { get; init; }

        [JsonPropertyName("sealCheckRequired")]
        [Description("Is the seal check required")]
        public bool SealCheckRequired { get; init; }

        [JsonPropertyName("sealCheck")]
        public SealCheck SealCheck { get; init; }

        [JsonPropertyName("sealCheckOverride")]
        public InspectionOverride SealCheckOverride { get; init; }
    }
}
