namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PartOne
    {
        [JsonPropertyName("typeOfImp")]
        [Description("")]
        public int TypeOfImp { get; set; }

        [JsonPropertyName("personResponsible")]
        [Description("")]
        public Party PersonResponsible { get; set; }

        [JsonPropertyName("customsReferenceNumber")]
        [Description("Customs reference number")]
        public string[] CustomsReferenceNumber { get; set; }

        [JsonPropertyName("containsWoodPackaging")]
        [Description("(Deprecated in IMTA-11832) Does the consignment contain wood packaging?")]
        public bool ContainsWoodPackaging { get; set; }

        [JsonPropertyName("consignmentArrived")]
        [Description("Has the consignment arrived at the BCP?")]
        public bool ConsignmentArrived { get; set; }

        [JsonPropertyName("consignor")]
        [Description("")]
        public EconomicOperator Consignor { get; set; }

        [JsonPropertyName("consignorTwo")]
        [Description("")]
        public EconomicOperator ConsignorTwo { get; set; }

        [JsonPropertyName("packer")]
        [Description("")]
        public EconomicOperator Packer { get; set; }

        [JsonPropertyName("consignee")]
        [Description("")]
        public EconomicOperator Consignee { get; set; }

        [JsonPropertyName("importer")]
        [Description("")]
        public EconomicOperator Importer { get; set; }

        [JsonPropertyName("placeOfDestination")]
        [Description("")]
        public EconomicOperator PlaceOfDestination { get; set; }

        [JsonPropertyName("pod")]
        [Description("")]
        public EconomicOperator Pod { get; set; }

        [JsonPropertyName("placeOfOriginHarvest")]
        [Description("")]
        public EconomicOperator PlaceOfOriginHarvest { get; set; }

        [JsonPropertyName("additionalPermanentAddresses")]
        [Description("List of additional permanent addresses")]
        public Array AdditionalPermanentAddresses { get; set; }

        [JsonPropertyName("cphNumber")]
        [Description("Charity Parish Holding number")]
        public string[] CphNumber { get; set; }

        [JsonPropertyName("importingFromCharity")]
        [Description("Is the importer importing from a charity?")]
        public bool ImportingFromCharity { get; set; }

        [JsonPropertyName("isPlaceOfDestinationThePermanentAddress")]
        [Description("Is the place of destination the permanent address?")]
        public bool IsPlaceOfDestinationThePermanentAddress { get; set; }

        [JsonPropertyName("isCatchCertificateRequired")]
        [Description("Is this catch certificate required?")]
        public bool IsCatchCertificateRequired { get; set; }

        [JsonPropertyName("isGvmsRoute")]
        [Description("Is GVMS route?")]
        public bool IsGvmsRoute { get; set; }

        [JsonPropertyName("purpose")]
        [Description("")]
        public Purpose Purpose { get; set; }

        [JsonPropertyName("pointOfEntry")]
        [Description("Either a Border-Inspection-Post or Designated-Point-Of-Entry, e.g. GBFXT1")]
        public string[] PointOfEntry { get; set; }

        [JsonPropertyName("pointOfEntryControlPoint")]
        [Description("A control point at the point of entry")]
        public string[] PointOfEntryControlPoint { get; set; }

        [JsonPropertyName("meansOfTransport")]
        [Description("")]
        public MeansOfTransport MeansOfTransport { get; set; }

        [JsonPropertyName("transporter")]
        [Description("")]
        public EconomicOperator Transporter { get; set; }

        [JsonPropertyName("transporterDetailsRequired")]
        [Description("Are transporter details required for this consignment")]
        public bool TransporterDetailsRequired { get; set; }

        [JsonPropertyName("meansOfTransportFromEntryPoint")]
        [Description("")]
        public MeansOfTransport MeansOfTransportFromEntryPoint { get; set; }

        [JsonPropertyName("estimatedJourneyTimeInMinutes")]
        [Description("Estimated journey time in minutes to point of entry")]
        public decimal EstimatedJourneyTimeInMinutes { get; set; }

        [JsonPropertyName("responsibleForTransport")]
        [Description("(Deprecated in IMTA-12139) Person who is responsible for transport")]
        public string[] ResponsibleForTransport { get; set; }

        [JsonPropertyName("veterinaryInformation")]
        [Description("")]
        public VeterinaryInformation VeterinaryInformation { get; set; }

        [JsonPropertyName("importerLocalReferenceNumber")]
        [Description("Reference number added by the importer")]
        public string[] ImporterLocalReferenceNumber { get; set; }

        [JsonPropertyName("route")]
        [Description("")]
        public Route Route { get; set; }

        [JsonPropertyName("sealsContainers")]
        [Description("Array that contains pair of seal number and container number")]
        public Array SealsContainers { get; set; }

        [JsonPropertyName("submissionDate")]
        [Description("Date and time when the notification was submitted")]
        public string[] SubmissionDate { get; set; }

        [JsonPropertyName("submittedBy")]
        [Description("")]
        public UserInformation SubmittedBy { get; set; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for whole notification")]
        public Array ConsignmentValidations { get; set; }

        [JsonPropertyName("complexCommoditySelected")]
        [Description("Was complex commodity selected. Indicating if importer provided commodity code.")]
        public bool ComplexCommoditySelected { get; set; }

        [JsonPropertyName("portOfEntry")]
        [Description("Entry port for EU Import notification.")]
        public string[] PortOfEntry { get; set; }

        [JsonPropertyName("portOfExit")]
        [Description("Exit Port for EU Import Notification.")]
        public string[] PortOfExit { get; set; }

        [JsonPropertyName("exitedPortOfOn")]
        [Description("Date of Port Exit for EU Import Notification.")]
        public string[] ExitedPortOfOn { get; set; }

        [JsonPropertyName("contactDetails")]
        [Description("")]
        public ContactDetails ContactDetails { get; set; }

        [JsonPropertyName("nominatedContacts")]
        [Description("List of nominated contacts to receive text and email notifications")]
        public Array NominatedContacts { get; set; }

        [JsonPropertyName("originalEstimatedDateTime")]
        [Description("Original estimated date time of arrival")]
        public string[] OriginalEstimatedDateTime { get; set; }

        [JsonPropertyName("billingInformation")]
        [Description("")]
        public BillingInformation BillingInformation { get; set; }

        [JsonPropertyName("isChargeable")]
        [Description("Indicates whether CUC applies to the notification")]
        public bool IsChargeable { get; set; }

        [JsonPropertyName("wasChargeable")]
        [Description("Indicates whether CUC previously applied to the notification")]
        public bool WasChargeable { get; set; }

        [JsonPropertyName("commonUserCharge")]
        [Description("")]
        public CommonUserCharge CommonUserCharge { get; set; }

        [JsonPropertyName("provideCtcMrn")]
        [Description("")]
        public int ProvideCtcMrn { get; set; }

        [JsonPropertyName("arrivedOn")]
        [Description("DateTime")]
        public string[] ArrivedOn { get; set; }

        [JsonPropertyName("departedOn")]
        [Description("DateTime")]
        public string[] DepartedOn { get; set; }
    }
}
