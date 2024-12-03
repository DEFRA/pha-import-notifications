#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class PartOne
{
    [JsonPropertyName("typeOfImp")]
    [JsonIgnore]
    [Description("Used to indicate what type of EU Import the notification is - Live Animals, Product Of Animal Origin or High Risk Food Not Of Animal Origin")]
    public PartOneTypeOfImpEnum? TypeOfImp { get; init; }

    [JsonPropertyName("personResponsible")]
    [Description("The individual who has submitted the notification")]
    public Party? PersonResponsible { get; init; }

    [JsonPropertyName("customsReferenceNumber")]
    [JsonIgnore]
    [Description("Customs reference number")]
    public string? CustomsReferenceNumber { get; init; }

    [JsonPropertyName("containsWoodPackaging")]
    [JsonIgnore]
    [Description("(Deprecated in IMTA-11832) Does the consignment contain wood packaging?")]
    public bool? ContainsWoodPackaging { get; init; }

    [JsonPropertyName("consignmentArrived")]
    [Description("Has the consignment arrived at the BCP?")]
    public bool? ConsignmentArrived { get; init; }

    [JsonPropertyName("consignor")]
    [Description("Person or Company that sends shipment")]
    public EconomicOperator? Consignor { get; init; }

    [JsonPropertyName("consignorTwo")]
    [JsonIgnore]
    [Description("Person or Company that sends shipment")]
    public EconomicOperator? ConsignorTwo { get; init; }

    [JsonPropertyName("packer")]
    [Description("Person or Company that packs the shipment")]
    public EconomicOperator? Packer { get; init; }

    [JsonPropertyName("consignee")]
    [Description("Person or Company that receives shipment")]
    public EconomicOperator? Consignee { get; init; }

    [JsonPropertyName("importer")]
    [Description("Person or Company that is importing the consignment")]
    public EconomicOperator? Importer { get; init; }

    [JsonPropertyName("placeOfDestination")]
    [Description("Where the shipment is to be sent? For IMP minimum 48 hour accommodation/holding location.")]
    public EconomicOperator? PlaceOfDestination { get; init; }

    [JsonPropertyName("pod")]
    [Description("A temporary place of destination for plants")]
    public EconomicOperator? Pod { get; init; }

    [JsonPropertyName("placeOfOriginHarvest")]
    [JsonIgnore]
    [Description("Place in which the animals or products originate")]
    public EconomicOperator? PlaceOfOriginHarvest { get; init; }

    [JsonPropertyName("additionalPermanentAddresses")]
    [JsonIgnore]
    [Description("List of additional permanent addresses")]
    public List<EconomicOperator>? AdditionalPermanentAddresses { get; init; }

    [JsonPropertyName("cphNumber")]
    [Description("Charity Parish Holding number")]
    public string? CphNumber { get; init; }

    [JsonPropertyName("importingFromCharity")]
    [JsonIgnore]
    [Description("Is the importer importing from a charity?")]
    public bool? ImportingFromCharity { get; init; }

    [JsonPropertyName("isPlaceOfDestinationThePermanentAddress")]
    [JsonIgnore]
    [Description("Is the place of destination the permanent address?")]
    public bool? IsPlaceOfDestinationThePermanentAddress { get; init; }

    [JsonPropertyName("isCatchCertificateRequired")]
    [Description("Is this catch certificate required?")]
    public bool? IsCatchCertificateRequired { get; init; }

    [JsonPropertyName("isGvmsRoute")]
    [Description("Is GVMS route?")]
    public bool? IsGvmsRoute { get; init; }

    [JsonPropertyName("purpose")]
    [Description("Purpose of consignment details")]
    public Purpose? Purpose { get; init; }

    [JsonPropertyName("pointOfEntry")]
    [Description("Either a Border-Inspection-Post or Designated-Point-Of-Entry, e.g. GBFXT1")]
    public string? PointOfEntry { get; init; }

    [JsonPropertyName("pointOfEntryControlPoint")]
    [Description("A control point at the point of entry")]
    public string? PointOfEntryControlPoint { get; init; }

    [JsonPropertyName("meansOfTransport")]
    [Description("How consignment is transported after BIP")]
    public MeansOfTransport? MeansOfTransport { get; init; }

    [JsonPropertyName("transporter")]
    [Description("Transporter of consignment details")]
    public EconomicOperator? Transporter { get; init; }

    [JsonPropertyName("transporterDetailsRequired")]
    [Description("Are transporter details required for this consignment")]
    public bool? TransporterDetailsRequired { get; init; }

    [JsonPropertyName("meansOfTransportFromEntryPoint")]
    [Description("Transport to BIP")]
    public MeansOfTransport? MeansOfTransportFromEntryPoint { get; init; }

    [JsonPropertyName("estimatedJourneyTimeInMinutes")]
    [Description("Estimated journey time in minutes to point of entry")]
    public decimal? EstimatedJourneyTimeInMinutes { get; init; }

    [JsonPropertyName("responsibleForTransport")]
    [JsonIgnore]
    [Description("(Deprecated in IMTA-12139) Person who is responsible for transport")]
    public string? ResponsibleForTransport { get; init; }

    [JsonPropertyName("veterinaryInformation")]
    [Description("Part 1 - Holds the information related to veterinary checks and details")]
    public VeterinaryInformation? VeterinaryInformation { get; init; }

    [JsonPropertyName("importerLocalReferenceNumber")]
    [Description("Reference number added by the importer")]
    public string? ImporterLocalReferenceNumber { get; init; }

    [JsonPropertyName("route")]
    [Description("Contains countries and transfer points that consignment is going through")]
    public Route? Route { get; init; }

    [JsonPropertyName("sealsContainers")]
    [JsonIgnore]
    [Description("Array that contains pair of seal number and container number")]
    public List<SealContainer>? SealsContainers { get; init; }

    [JsonPropertyName("submittedOn")]
    [Description("Date and time when the notification was submitted")]
    public DateTime? SubmittedOn { get; init; }

    [JsonPropertyName("submittedBy")]
    [Description("Information about user who submitted notification")]
    public UserInformation? SubmittedBy { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for whole notification")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("complexCommoditySelected")]
    [JsonIgnore]
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
    [Description("Person to be contacted if there is an issue with the consignment")]
    public ContactDetails? ContactDetails { get; init; }

    [JsonPropertyName("nominatedContacts")]
    [Description("List of nominated contacts to receive text and email notifications")]
    public List<NominatedContact>? NominatedContacts { get; init; }

    [JsonPropertyName("originalEstimatedOn")]
    [Description("Original estimated date time of arrival")]
    public DateTime? OriginalEstimatedOn { get; init; }

    [JsonPropertyName("billingInformation")]
    public BillingInformation? BillingInformation { get; init; }

    [JsonPropertyName("isChargeable")]
    [Description("Indicates whether CUC applies to the notification")]
    public bool? IsChargeable { get; init; }

    [JsonPropertyName("wasChargeable")]
    [Description("Indicates whether CUC previously applied to the notification")]
    public bool? WasChargeable { get; init; }

    [JsonPropertyName("commonUserCharge")]
    public CommonUserCharge? CommonUserCharge { get; init; }

    [JsonPropertyName("provideCtcMrn")]
    [Description("When the NCTS MRN will be added for the Common Transit Convention (CTC)")]
    public PartOneProvideCtcMrnEnum? ProvideCtcMrn { get; init; }

    [JsonPropertyName("arrivesAt")]
    [Description("DateTime")]
    public DateTime? ArrivesAt { get; init; }

    [JsonPropertyName("departedOn")]
    [Description("DateTime")]
    public DateTime? DepartedOn { get; init; }
}
