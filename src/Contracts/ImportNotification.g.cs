namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ImportNotification
    {
        [JsonPropertyName("_Etag")]
        public string _Etag { get; set; }

        [JsonPropertyName("auditEntries")]
        public Array AuditEntries { get; set; }

        [JsonPropertyName("relationships")]
        public NotificationTdmRelationships Relationships { get; set; }

        [JsonPropertyName("commoditiesSummary")]
        public Commodities CommoditiesSummary { get; set; }

        [JsonPropertyName("commodities")]
        public Array Commodities { get; set; }

        [JsonPropertyName("_Ts")]
        public string _Ts { get; set; }

        [JsonPropertyName("_PointOfEntry")]
        public string _PointOfEntry { get; set; }

        [JsonPropertyName("_PointOfEntryControlPoint")]
        public string _PointOfEntryControlPoint { get; set; }

        [JsonPropertyName("_MatchReference")]
        public int _MatchReference { get; set; }

        [JsonPropertyName("ipaffsId")]
        [Description("The IPAFFS ID number for this notification.")]
        public int IpaffsId { get; set; }

        [JsonPropertyName("etag")]
        [Description("The etag for this notification.")]
        public string Etag { get; set; }

        [JsonPropertyName("externalReferences")]
        [Description("List of external references, which relate to downstream services")]
        public Array ExternalReferences { get; set; }

        [JsonPropertyName("referenceNumber")]
        [Description("Reference number of the notification")]
        public string ReferenceNumber { get; set; }

        [JsonPropertyName("version")]
        [Description("Current version of the notification")]
        public int Version { get; set; }

        [JsonPropertyName("lastUpdated")]
        [Description("Date when the notification was last updated.")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public UserInformation LastUpdatedBy { get; set; }

        [JsonPropertyName("importNotificationType")]
        public int ImportNotificationType { get; set; }

        [JsonPropertyName("replaces")]
        [Description("Reference number of notification that was replaced by this one")]
        public string Replaces { get; set; }

        [JsonPropertyName("replacedBy")]
        [Description("Reference number of notification that replaced this one")]
        public string ReplacedBy { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("splitConsignment")]
        public SplitConsignment SplitConsignment { get; set; }

        [JsonPropertyName("childNotification")]
        [Description("Is this notification a child of a split consignment?")]
        public bool ChildNotification { get; set; }

        [JsonPropertyName("riskAssessment")]
        public RiskAssessmentResult RiskAssessment { get; set; }

        [JsonPropertyName("journeyRiskCategorisation")]
        public JourneyRiskCategorisationResult JourneyRiskCategorisation { get; set; }

        [JsonPropertyName("isHighRiskEuImport")]
        [Description("Is this notification a high risk notification from the EU/EEA?")]
        public bool IsHighRiskEuImport { get; set; }

        [JsonPropertyName("partOne")]
        public PartOne PartOne { get; set; }

        [JsonPropertyName("decisionBy")]
        public UserInformation DecisionBy { get; set; }

        [JsonPropertyName("decisionDate")]
        [Description("Date when the notification was validated or rejected")]
        public string DecisionDate { get; set; }

        [JsonPropertyName("partTwo")]
        public PartTwo PartTwo { get; set; }

        [JsonPropertyName("partThree")]
        public PartThree PartThree { get; set; }

        [JsonPropertyName("officialVeterinarian")]
        [Description("Official veterinarian")]
        public string OfficialVeterinarian { get; set; }

        [JsonPropertyName("consignmentValidations")]
        [Description("Validation messages for whole notification")]
        public Array ConsignmentValidations { get; set; }

        [JsonPropertyName("agencyOrganisationId")]
        [Description("Organisation id which the agent user belongs to, stored against each notification which has been raised on behalf of another organisation")]
        public string AgencyOrganisationId { get; set; }

        [JsonPropertyName("riskDecisionLockingTime")]
        [Description("Date and Time when risk decision was locked")]
        public string RiskDecisionLockingTime { get; set; }

        [JsonPropertyName("isRiskDecisionLocked")]
        [Description("is the risk decision locked?")]
        public bool IsRiskDecisionLocked { get; set; }

        [JsonPropertyName("isBulkUploadInProgress")]
        [Description("Boolean flag that indicates whether a bulk upload is in progress")]
        public bool IsBulkUploadInProgress { get; set; }

        [JsonPropertyName("requestId")]
        [Description("Request UUID to trace bulk upload")]
        public string RequestId { get; set; }

        [JsonPropertyName("isCdsFullMatched")]
        [Description("Have all commodities been matched with corresponding CDS declaration(s)")]
        public bool IsCdsFullMatched { get; set; }

        [JsonPropertyName("chedTypeVersion")]
        [Description("The version of the ched type the notification was created with")]
        public int ChedTypeVersion { get; set; }

        [JsonPropertyName("isGMRMatched")]
        [Description("Indicates whether a CHED has been matched with a GVMS GMR via DMP")]
        public bool IsGMRMatched { get; set; }
    }
}