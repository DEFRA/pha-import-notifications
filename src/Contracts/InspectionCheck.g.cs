#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record InspectionCheck
{
    [JsonPropertyName("type")]
    [Description("Type of check")]
    public InspectionCheckTypeEnum? Type { get; init; }

    [JsonPropertyName("status")]
    [Description("Status of the check")]
    public InspectionCheckStatusEnum? Status { get; init; }

    [JsonPropertyName("reason")]
    [Description("Reason for the status if applicable")]
    public string? Reason { get; init; }

    [JsonPropertyName("otherReason")]
    [Description("Other reason text when selected reason is 'Other'")]
    public string? OtherReason { get; init; }

    [JsonPropertyName("isSelectedForChecks")]
    [Description("Has commodity been selected for checks?")]
    public bool? IsSelectedForChecks { get; init; }

    [JsonPropertyName("hasChecksComplete")]
    [Description("Has commodity completed this type of check")]
    public bool? HasChecksComplete { get; init; }
}
