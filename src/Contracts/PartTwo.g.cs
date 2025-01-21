#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record PartTwo
{
    [JsonPropertyName("decision")]
    [Description("Decision on the consignment")]
    public Decision? Decision { get; init; }

    [JsonPropertyName("consignmentCheck")]
    [Description("Consignment check")]
    public ConsignmentCheck? ConsignmentCheck { get; init; }

    [JsonPropertyName("impactOfTransportOnAnimals")]
    [Description("Checks of impact of transport on animals")]
    public ImpactOfTransportOnAnimals? ImpactOfTransportOnAnimals { get; init; }

    [JsonPropertyName("laboratoryTestsRequired")]
    [Description("Are laboratory tests required")]
    public bool? LaboratoryTestsRequired { get; init; }

    [JsonPropertyName("laboratoryTests")]
    [Description("Laboratory tests information details")]
    public LaboratoryTests? LaboratoryTests { get; init; }

    [JsonPropertyName("resealedContainersIncluded")]
    [Description("Are the containers resealed")]
    public bool? ResealedContainersIncluded { get; init; }

    [JsonPropertyName("resealedContainers")]
    [JsonIgnore]
    [Description("(Deprecated - To be removed as part of IMTA-6256) Resealed containers information details")]
    public List<string>? ResealedContainers { get; init; }

    [JsonPropertyName("resealedContainersMappings")]
    [Description("Resealed containers information details")]
    public List<SealContainer>? ResealedContainersMappings { get; init; }

    [JsonPropertyName("controlAuthority")]
    [Description("Control Authority information details")]
    public ControlAuthority? ControlAuthority { get; init; }

    [JsonPropertyName("controlledDestination")]
    [Description("Controlled destination")]
    public EconomicOperator? ControlledDestination { get; init; }

    [JsonPropertyName("bipLocalReferenceNumber")]
    [Description("Local reference number at BIP")]
    public string? BipLocalReferenceNumber { get; init; }

    [JsonPropertyName("signedOnBehalfOf")]
    [Description("Part 2 - Sometimes other user can sign decision on behalf of another user")]
    public string? SignedOnBehalfOf { get; init; }

    [JsonPropertyName("onwardTransportation")]
    [Description("Onward transportation")]
    public string? OnwardTransportation { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for Part 2 - Decision")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("checkedOn")]
    [Description("User entered date when the checks were completed")]
    public DateTime? CheckedOn { get; init; }

    [JsonPropertyName("accompanyingDocuments")]
    [Description("Accompanying documents")]
    public List<AccompanyingDocument>? AccompanyingDocuments { get; init; }

    [JsonPropertyName("phsiAutoCleared")]
    [Description("Have the PHSI regulated commodities been auto cleared?")]
    public bool? PhsiAutoCleared { get; init; }

    [JsonPropertyName("hmiAutoCleared")]
    [Description("Have the HMI regulated commodities been auto cleared?")]
    public bool? HmiAutoCleared { get; init; }

    [JsonPropertyName("inspectionRequired")]
    [Description("Inspection required")]
    public InspectionRequiredEnum? InspectionRequired { get; init; }

    [JsonPropertyName("inspectionOverride")]
    [Description("Details about the manual inspection override")]
    public InspectionOverride? InspectionOverride { get; init; }

    [JsonPropertyName("autoClearedOn")]
    [Description("Date of autoclearance")]
    public DateTime? AutoClearedOn { get; init; }
}
