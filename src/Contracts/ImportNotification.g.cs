#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ImportNotification
{
    [JsonPropertyName("auditEntries")]
    public List<AuditEntry>? AuditEntries { get; init; }

    [JsonPropertyName("commoditiesSummary")]
    public required Commodities CommoditiesSummary { get; init; }

    [JsonPropertyName("commodities")]
    public List<CommodityComplement>? Commodities { get; init; }

    [JsonPropertyName("ipaffsId")]
    [Description("The IPAFFS ID number for this notification.")]
    public int? Id { get; init; }

    [JsonPropertyName("etag")]
    [Description("The etag for this notification.")]
    public string? Etag { get; init; }

    [JsonPropertyName("externalReferences")]
    [Description("List of external references, which relate to downstream services")]
    public List<ExternalReference>? ExternalReferences { get; init; }

    [JsonPropertyName("version")]
    [Description("Current version of the notification")]
    public int? Version { get; init; }

    [JsonPropertyName("lastUpdated")]
    [Description("Date when the notification was last updated.")]
    public DateTime? LastUpdated { get; init; }

    [JsonPropertyName("lastUpdatedBy")]
    public required UserInformation LastUpdatedBy { get; init; }

    [JsonPropertyName("ipaffsType")]
    public required NotificationTypeEnum Type { get; init; }

    [JsonPropertyName("replaces")]
    [Description("Reference number of notification that was replaced by this one")]
    public string? Replaces { get; init; }

    [JsonPropertyName("replacedBy")]
    [Description("Reference number of notification that replaced this one")]
    public string? ReplacedBy { get; init; }

    [JsonPropertyName("status")]
    public required NotificationStatusEnum Status { get; init; }

    [JsonPropertyName("splitConsignment")]
    public required SplitConsignment SplitConsignment { get; init; }

    [JsonPropertyName("childNotification")]
    [Description("Is this notification a child of a split consignment?")]
    public bool? ChildNotification { get; init; }

    [JsonPropertyName("riskAssessment")]
    public required RiskAssessmentResult RiskAssessment { get; init; }

    [JsonPropertyName("journeyRiskCategorisation")]
    public required JourneyRiskCategorisationResult JourneyRiskCategorisation { get; init; }

    [JsonPropertyName("isHighRiskEuImport")]
    [Description("Is this notification a high risk notification from the EU/EEA?")]
    public bool? IsHighRiskEuImport { get; init; }

    [JsonPropertyName("partOne")]
    public required PartOne PartOne { get; init; }

    [JsonPropertyName("decisionBy")]
    public required UserInformation DecisionBy { get; init; }

    [JsonPropertyName("decisionDate")]
    [Description("Date when the notification was validated or rejected")]
    public DateTime? DecisionDate { get; init; }

    [JsonPropertyName("partTwo")]
    public required PartTwo PartTwo { get; init; }

    [JsonPropertyName("partThree")]
    public required PartThree PartThree { get; init; }

    [JsonPropertyName("officialVeterinarian")]
    [Description("Official veterinarian")]
    public string? OfficialVeterinarian { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for whole notification")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("agencyOrganisationId")]
    [Description("Organisation id which the agent user belongs to, stored against each notification which has been raised on behalf of another organisation")]
    public string? AgencyOrganisationId { get; init; }

    [JsonPropertyName("riskDecisionLockingTime")]
    [Description("Date and Time when risk decision was locked")]
    public DateTime? RiskDecisionLockingTime { get; init; }

    [JsonPropertyName("isRiskDecisionLocked")]
    [Description("is the risk decision locked?")]
    public bool? IsRiskDecisionLocked { get; init; }

    [JsonPropertyName("isBulkUploadInProgress")]
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

    [JsonPropertyName("isGmrMatched")]
    [Description("Indicates whether a CHED has been matched with a GVMS GMR via DMP")]
    public bool? IsGmrMatched { get; init; }
}
