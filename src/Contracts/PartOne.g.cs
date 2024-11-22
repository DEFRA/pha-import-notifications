using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class PartOne
{
    [JsonPropertyName("typeOfImp")]
    public required PartOneTypeOfImpEnum TypeOfImp { get; init; }

    [JsonPropertyName("personResponsible")]
    public required Party PersonResponsible { get; init; }

    [JsonPropertyName("customsReferenceNumber")]
    [Description("Customs reference number")]
    public string? CustomsReferenceNumber { get; init; }

    [JsonPropertyName("containsWoodPackaging")]
    [Description("(Deprecated in IMTA-11832) Does the consignment contain wood packaging?")]
    public bool? ContainsWoodPackaging { get; init; }

    [JsonPropertyName("consignmentArrived")]
    [Description("Has the consignment arrived at the BCP?")]
    public bool? ConsignmentArrived { get; init; }

    [JsonPropertyName("consignor")]
    public required EconomicOperator Consignor { get; init; }

    [JsonPropertyName("consignorTwo")]
    public required EconomicOperator ConsignorTwo { get; init; }

    [JsonPropertyName("packer")]
    public required EconomicOperator Packer { get; init; }

    [JsonPropertyName("consignee")]
    public required EconomicOperator Consignee { get; init; }

    [JsonPropertyName("importer")]
    public required EconomicOperator Importer { get; init; }

    [JsonPropertyName("placeOfDestination")]
    public required EconomicOperator PlaceOfDestination { get; init; }

    [JsonPropertyName("pod")]
    public required EconomicOperator Pod { get; init; }

    [JsonPropertyName("placeOfOriginHarvest")]
    public required EconomicOperator PlaceOfOriginHarvest { get; init; }

    [JsonPropertyName("additionalPermanentAddresses")]
    [Description("List of additional permanent addresses")]
    public List<EconomicOperator>? AdditionalPermanentAddresses { get; init; }

    [JsonPropertyName("cphNumber")]
    [Description("Charity Parish Holding number")]
    public string? CphNumber { get; init; }

    [JsonPropertyName("importingFromCharity")]
    [Description("Is the importer importing from a charity?")]
    public bool? ImportingFromCharity { get; init; }

    [JsonPropertyName("isPlaceOfDestinationThePermanentAddress")]
    [Description("Is the place of destination the permanent address?")]
    public bool? IsPlaceOfDestinationThePermanentAddress { get; init; }

    [JsonPropertyName("isCatchCertificateRequired")]
    [Description("Is this catch certificate required?")]
    public bool? IsCatchCertificateRequired { get; init; }

    [JsonPropertyName("isGvmsRoute")]
    [Description("Is GVMS route?")]
    public bool? IsGvmsRoute { get; init; }

    [JsonPropertyName("purpose")]
    public required Purpose Purpose { get; init; }

    [JsonPropertyName("pointOfEntry")]
    [Description("Either a Border-Inspection-Post or Designated-Point-Of-Entry, e.g. GBFXT1")]
    public string? PointOfEntry { get; init; }

    [JsonPropertyName("pointOfEntryControlPoint")]
    [Description("A control point at the point of entry")]
    public string? PointOfEntryControlPoint { get; init; }

    [JsonPropertyName("meansOfTransport")]
    public required MeansOfTransport MeansOfTransport { get; init; }

    [JsonPropertyName("transporter")]
    public required EconomicOperator Transporter { get; init; }

    [JsonPropertyName("transporterDetailsRequired")]
    [Description("Are transporter details required for this consignment")]
    public bool? TransporterDetailsRequired { get; init; }

    [JsonPropertyName("meansOfTransportFromEntryPoint")]
    public required MeansOfTransport MeansOfTransportFromEntryPoint { get; init; }

    [JsonPropertyName("estimatedJourneyTimeInMinutes")]
    [Description("Estimated journey time in minutes to point of entry")]
    public decimal? EstimatedJourneyTimeInMinutes { get; init; }

    [JsonPropertyName("responsibleForTransport")]
    [Description("(Deprecated in IMTA-12139) Person who is responsible for transport")]
    public string? ResponsibleForTransport { get; init; }

    [JsonPropertyName("veterinaryInformation")]
    public required VeterinaryInformation VeterinaryInformation { get; init; }

    [JsonPropertyName("importerLocalReferenceNumber")]
    [Description("Reference number added by the importer")]
    public string? ImporterLocalReferenceNumber { get; init; }

    [JsonPropertyName("route")]
    public required Route Route { get; init; }

    [JsonPropertyName("sealsContainers")]
    [Description("Array that contains pair of seal number and container number")]
    public List<SealContainer>? SealsContainers { get; init; }

    [JsonPropertyName("submissionDate")]
    [Description("Date and time when the notification was submitted")]
    public DateTime? SubmissionDate { get; init; }

    [JsonPropertyName("submittedBy")]
    public required UserInformation SubmittedBy { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for whole notification")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("complexCommoditySelected")]
    [Description("Was complex commodity selected. Indicating if importer provided commodity code.")]
    public bool? ComplexCommoditySelected { get; init; }

    [JsonPropertyName("portOfEntry")]
    [Description("Entry port for EU Import notification.")]
    public string? PortOfEntry { get; init; }

    [JsonPropertyName("portOfExit")]
    [Description("Exit Port for EU Import Notification.")]
    public string? PortOfExit { get; init; }

    [JsonPropertyName("exitedPortOfOn")]
    [Description("Date of Port Exit for EU Import Notification.")]
    public DateTime? ExitedPortOfOn { get; init; }

    [JsonPropertyName("contactDetails")]
    public required ContactDetails ContactDetails { get; init; }

    [JsonPropertyName("nominatedContacts")]
    [Description("List of nominated contacts to receive text and email notifications")]
    public List<NominatedContact>? NominatedContacts { get; init; }

    [JsonPropertyName("originalEstimatedDateTime")]
    [Description("Original estimated date time of arrival")]
    public DateTime? OriginalEstimatedDateTime { get; init; }

    [JsonPropertyName("billingInformation")]
    public required BillingInformation BillingInformation { get; init; }

    [JsonPropertyName("isChargeable")]
    [Description("Indicates whether CUC applies to the notification")]
    public bool? IsChargeable { get; init; }

    [JsonPropertyName("wasChargeable")]
    [Description("Indicates whether CUC previously applied to the notification")]
    public bool? WasChargeable { get; init; }

    [JsonPropertyName("commonUserCharge")]
    public required CommonUserCharge CommonUserCharge { get; init; }

    [JsonPropertyName("provideCtcMrn")]
    public required PartOneProvideCtcMrnEnum ProvideCtcMrn { get; init; }

    [JsonPropertyName("arrivedOn")]
    [Description("DateTime")]
    public DateTime? ArrivedOn { get; init; }

    [JsonPropertyName("departedOn")]
    [Description("DateTime")]
    public DateTime? DepartedOn { get; init; }
}
