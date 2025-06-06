#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportPreNotification
{
    [JsonPropertyName("ipaffsId")]
    [Description("The IPAFFS ID number for this notification.")]
    public int? IpaffsId { get; init; }

    [JsonPropertyName("etag")]
    [Description("The etag for this notification.")]
    public string? Etag { get; init; }

    [JsonPropertyName("externalReferences")]
    [Description("List of external references, which relate to downstream services")]
    public List<ExternalReference>? ExternalReferences { get; init; }

    [JsonPropertyName("referenceNumber")]
    [Description("Reference number of the notification")]
    public string? ReferenceNumber { get; init; }

    [JsonPropertyName("version")]
    [Description("Current version of the notification")]
    public int? Version { get; init; }

    [JsonPropertyName("updatedSource")]
    [Description("Date when the notification was last updated.")]
    public DateTime? UpdatedSource { get; init; }

    [JsonPropertyName("lastUpdatedBy")]
    [Description("User entity whose update was last")]
    public UserInformation? LastUpdatedBy { get; init; }

    [JsonPropertyName("importNotificationType")]
    [ExampleValue("CVEDA")]
    [ExampleValue("CVEDP")]
    [ExampleValue("CHEDPP")]
    [ExampleValue("CED")]
    [Description("The Type of notification that has been submitted. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ImportNotificationType { get; init; }

    [JsonPropertyName("replaces")]
    [Description("Reference number of notification that was replaced by this one")]
    public string? Replaces { get; init; }

    [JsonPropertyName("replacedBy")]
    [Description("Reference number of notification that replaced this one")]
    public string? ReplacedBy { get; init; }

    [JsonPropertyName("status")]
    [ExampleValue("SUBMITTED")]
    [ExampleValue("VALIDATED")]
    [ExampleValue("REJECTED")]
    [ExampleValue("IN_PROGRESS")]
    [ExampleValue("AMEND")]
    [ExampleValue("MODIFY")]
    [ExampleValue("REPLACED")]
    [ExampleValue("CANCELLED")]
    [ExampleValue("DELETED")]
    [ExampleValue("PARTIALLY_REJECTED")]
    [ExampleValue("SPLIT_CONSIGNMENT")]
    [Description("Current status of the notification. When created by an importer, the notification has the status 'SUBMITTED'. Before submission of the notification it has the status 'DRAFT'. When the BIP starts validation of the notification it has the status 'IN PROGRESS' Once the BIP validates the notification, it gets the status 'VALIDATED'. 'AMEND' is set when the Part-1 user is modifying the notification. 'MODIFY' is set when Part-2 user is modifying the notification. Replaced - When the notification is replaced by another notification. Rejected - Notification moves to Rejected status when rejected by a Part-2 user. Possible values taken from IPAFFS schema version 17.5.")]
    public string? Status { get; init; }

    [JsonPropertyName("splitConsignment")]
    [Description("Present if the consignment has been split")]
    public SplitConsignment? SplitConsignment { get; init; }

    [JsonPropertyName("childNotification")]
    [Description("Is this notification a child of a split consignment?")]
    public bool? ChildNotification { get; init; }

    [JsonPropertyName("riskAssessment")]
    [Description("Result of risk assessment by the risk scorer")]
    public RiskAssessmentResult? RiskAssessment { get; init; }

    [JsonPropertyName("journeyRiskCategorisation")]
    [Description("Details of the risk categorisation level for a notification")]
    public JourneyRiskCategorisationResult? JourneyRiskCategorisation { get; init; }

    [JsonPropertyName("isHighRiskEuImport")]
    [Description("Is this notification a high risk notification from the EU/EEA?")]
    public bool? IsHighRiskEuImport { get; init; }

    [JsonPropertyName("partOne")]
    public PartOne? PartOne { get; init; }

    [JsonPropertyName("decisionBy")]
    [Description("Information about the user who set the decision in Part 2")]
    public UserInformation? DecisionBy { get; init; }

    [JsonPropertyName("decisionDate")]
    [Description("Date when the notification was validated or rejected")]
    public string? DecisionDate { get; init; }

    [JsonPropertyName("partTwo")]
    [Description("Part of the notification which contains information filled by inspector at BIP/DPE")]
    public PartTwo? PartTwo { get; init; }

    [JsonPropertyName("partThree")]
    [Description("Part of the notification which contains information filled by LVU if control of consignment is needed.")]
    public PartThree? PartThree { get; init; }

    [JsonPropertyName("officialVeterinarian")]
    [Description("Official veterinarian")]
    public string? OfficialVeterinarian { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for whole notification")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("agencyOrganisationId")]
    [Description("Organisation id which the agent user belongs to, stored against each notification which has been raised on behalf of another organisation")]
    public string? AgencyOrganisationId { get; init; }

    [JsonPropertyName("riskDecisionLockedOn")]
    [Description("Date and Time when risk decision was locked")]
    public DateTime? RiskDecisionLockedOn { get; init; }

    [JsonPropertyName("isRiskDecisionLocked")]
    [Description("is the risk decision locked?")]
    public bool? IsRiskDecisionLocked { get; init; }

    [JsonPropertyName("isAutoClearanceExempted")]
    [Description("Boolean flag that indicates whether CHED is exempted from autoclearance")]
    public bool? IsAutoClearanceExempted { get; init; }

    [JsonPropertyName("isBulkUploadInProgress")]
    [JsonIgnore]
    [Description("Boolean flag that indicates whether a bulk upload is in progress")]
    public bool? IsBulkUploadInProgress { get; init; }

    [JsonPropertyName("requestId")]
    [Description("Request UUID to trace bulk upload")]
    public string? RequestId { get; init; }

    [JsonPropertyName("isCdsFullMatched")]
    [Description("Have all commodities been matched with corresponding CDS declaration(s)")]
    public bool? IsCdsFullMatched { get; init; }

    [JsonPropertyName("chedTypeVersion")]
    [Description("The version of the ched type the notification was created with")]
    public int? ChedTypeVersion { get; init; }

    [JsonPropertyName("isGMRMatched")]
    [Description("Indicates whether a CHED has been matched with a GVMS GMR via DMP")]
    public bool? IsGMRMatched { get; init; }
}
