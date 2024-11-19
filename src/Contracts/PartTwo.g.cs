namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartTwo
    {
        [JsonPropertyName("decision")]
        [Description("")]
        public Decision Decision { get; set; }

        [JsonPropertyName("consignmentCheck")]
        [Description("")]
        public ConsignmentCheck ConsignmentCheck { get; set; }

        [JsonPropertyName("impactOfTransportOnAnimals")]
        [Description("")]
        public ImpactOfTransportOnAnimals ImpactOfTransportOnAnimals { get; set; }

        [JsonPropertyName("laboratoryTestsRequired")]
        [Description("Are laboratory tests required")]
        public bool LaboratoryTestsRequired { get; set; }

        [JsonPropertyName("laboratoryTests")]
        [Description("")]
        public LaboratoryTests LaboratoryTests { get; set; }

        [JsonPropertyName("resealedContainersIncluded")]
        [Description("Are the containers resealed")]
        public bool ResealedContainersIncluded { get; set; }

        [JsonPropertyName("resealedContainers")]
        [Description("(Deprecated - To be removed as part of IMTA-6256) Resealed containers information details")]
        public Array ResealedContainers { get; set; }

        [JsonPropertyName("resealedContainersMappings")]
        [Description("Resealed containers information details")]
        public Array ResealedContainersMappings { get; set; }

        [JsonPropertyName("controlAuthority")]
        [Description("")]
        public ControlAuthority ControlAuthority { get; set; }

        [JsonPropertyName("controlledDestination")]
        [Description("")]
        public EconomicOperator ControlledDestination { get; set; }

        [JsonPropertyName("bipLocalReferenceNumber")]
        [Description("Local reference number at BIP")]
        public string BipLocalReferenceNumber { get; set; }

        [JsonPropertyName("signedOnBehalfOf")]
        [Description("Part 2 - Sometimes other user can sign decision on behalf of another user")]
        public string SignedOnBehalfOf { get; set; }

        [JsonPropertyName("onwardTransportation")]
        [Description("Onward transportation")]
        public string OnwardTransportation { get; set; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for Part 2 - Decision")]
        public Array ConsignmentValidations { get; set; }

        [JsonPropertyName("checkedOn")]
        [Description("User entered date when the checks were completed")]
        public string CheckedOn { get; set; }

        [JsonPropertyName("accompanyingDocuments")]
        [Description("Accompanying documents")]
        public Array AccompanyingDocuments { get; set; }

        [JsonPropertyName("phsiAutoCleared")]
        [Description("Have the PHSI regulated commodities been auto cleared?")]
        public bool PhsiAutoCleared { get; set; }

        [JsonPropertyName("hmiAutoCleared")]
        [Description("Have the HMI regulated commodities been auto cleared?")]
        public bool HmiAutoCleared { get; set; }

        [JsonPropertyName("inspectionRequired")]
        [Description("Inspection required")]
        public string InspectionRequired { get; set; }

        [JsonPropertyName("inspectionOverride")]
        [Description("")]
        public InspectionOverride InspectionOverride { get; set; }

        [JsonPropertyName("autoClearedOn")]
        [Description("Date of autoclearance")]
        public string AutoClearedOn { get; set; }
    }
}
