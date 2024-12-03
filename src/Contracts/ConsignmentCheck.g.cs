#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ConsignmentCheck
{
    [JsonPropertyName("euStandard")]
    [Description("Does it conform EU standards")]
    public string? EuStandard { get; init; }

    [JsonPropertyName("additionalGuarantees")]
    [Description("Result of additional guarantees")]
    public string? AdditionalGuarantees { get; init; }

    [JsonPropertyName("documentCheckResult")]
    [Description("Result of document check")]
    public string? DocumentCheckResult { get; init; }

    [JsonPropertyName("nationalRequirements")]
    [Description("Result of national requirements check")]
    public string? NationalRequirements { get; init; }

    [JsonPropertyName("identityCheckDone")]
    [Description("Was identity check done")]
    public bool? IdentityCheckDone { get; init; }

    [JsonPropertyName("identityCheckType")]
    [Description("Type of identity check performed")]
    public ConsignmentCheckIdentityCheckTypeEnum? IdentityCheckType { get; init; }

    [JsonPropertyName("identityCheckResult")]
    [Description("Result of identity check")]
    public string? IdentityCheckResult { get; init; }

    [JsonPropertyName("identityCheckNotDoneReason")]
    [Description("What was the reason for skipping identity check")]
    public ConsignmentCheckIdentityCheckNotDoneReasonEnum? IdentityCheckNotDoneReason { get; init; }

    [JsonPropertyName("physicalCheckDone")]
    [Description("Was physical check done")]
    public bool? PhysicalCheckDone { get; init; }

    [JsonPropertyName("physicalCheckResult")]
    [Description("Result of physical check")]
    public string? PhysicalCheckResult { get; init; }

    [JsonPropertyName("physicalCheckNotDoneReason")]
    [Description("What was the reason for skipping physical check")]
    public ConsignmentCheckPhysicalCheckNotDoneReasonEnum? PhysicalCheckNotDoneReason { get; init; }

    [JsonPropertyName("physicalCheckOtherText")]
    [Description("Other reason to not do physical check")]
    public string? PhysicalCheckOtherText { get; init; }

    [JsonPropertyName("welfareCheck")]
    [Description("Welfare check")]
    public string? WelfareCheck { get; init; }

    [JsonPropertyName("numberOfAnimalsChecked")]
    [Description("Number of animals checked")]
    public int? NumberOfAnimalsChecked { get; init; }

    [JsonPropertyName("laboratoryCheckDone")]
    [Description("Were laboratory tests done")]
    public bool? LaboratoryCheckDone { get; init; }

    [JsonPropertyName("laboratoryCheckResult")]
    [Description("Result of laboratory tests")]
    public string? LaboratoryCheckResult { get; init; }
}
