namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartTwo
    {
        [JsonPropertyName("decision")]
        public Decision Decision { get; init; }

        [JsonPropertyName("consignmentCheck")]
        public ConsignmentCheck ConsignmentCheck { get; init; }

        [JsonPropertyName("impactOfTransportOnAnimals")]
        public ImpactOfTransportOnAnimals ImpactOfTransportOnAnimals { get; init; }

        [JsonPropertyName("laboratoryTestsRequired")]
        [Description("Are laboratory tests required")]
        public bool LaboratoryTestsRequired { get; init; }

        [JsonPropertyName("laboratoryTests")]
        public LaboratoryTests LaboratoryTests { get; init; }

        [JsonPropertyName("resealedContainersIncluded")]
        [Description("Are the containers resealed")]
        public bool ResealedContainersIncluded { get; init; }

        [JsonPropertyName("resealedContainers")]
        [Description("(Deprecated - To be removed as part of IMTA-6256) Resealed containers information details")]
        public Array ResealedContainers { get; init; }

        [JsonPropertyName("resealedContainersMappings")]
        [Description("Resealed containers information details")]
        public Array ResealedContainersMappings { get; init; }

        [JsonPropertyName("controlAuthority")]
        public ControlAuthority ControlAuthority { get; init; }

        [JsonPropertyName("controlledDestination")]
        public EconomicOperator ControlledDestination { get; init; }

        [JsonPropertyName("bipLocalReferenceNumber")]
        [Description("Local reference number at BIP")]
        public string BipLocalReferenceNumber { get; init; }

        [JsonPropertyName("signedOnBehalfOf")]
        [Description("Part 2 - Sometimes other user can sign decision on behalf of another user")]
        public string SignedOnBehalfOf { get; init; }

        [JsonPropertyName("onwardTransportation")]
        [Description("Onward transportation")]
        public string OnwardTransportation { get; init; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for Part 2 - Decision")]
        public Array ConsignmentValidations { get; init; }

        [JsonPropertyName("checkedOn")]
        [Description("User entered date when the checks were completed")]
        public string CheckedOn { get; init; }

        [JsonPropertyName("accompanyingDocuments")]
        [Description("Accompanying documents")]
        public Array AccompanyingDocuments { get; init; }

        [JsonPropertyName("phsiAutoCleared")]
        [Description("Have the PHSI regulated commodities been auto cleared?")]
        public bool PhsiAutoCleared { get; init; }

        [JsonPropertyName("hmiAutoCleared")]
        [Description("Have the HMI regulated commodities been auto cleared?")]
        public bool HmiAutoCleared { get; init; }

        [JsonPropertyName("inspectionRequired")]
        [Description("Inspection required")]
        public string InspectionRequired { get; init; }

        [JsonPropertyName("inspectionOverride")]
        public InspectionOverride InspectionOverride { get; init; }

        [JsonPropertyName("autoClearedOn")]
        [Description("Date of autoclearance")]
        public string AutoClearedOn { get; init; }
    }
}
