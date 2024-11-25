#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class InspectionCheck
{
    [JsonPropertyName("type")]
    public InspectionCheckTypeEnum Type { get; set; }

    [JsonPropertyName("status")]
    public InspectionCheckStatusEnum Status { get; set; }

    [JsonPropertyName("reason")]
    [Description("Reason for the status if applicable")]
    public string? Reason { get; set; }

    [JsonPropertyName("otherReason")]
    [Description("Other reason text when selected reason is 'Other'")]
    public string? OtherReason { get; set; }

    [JsonPropertyName("isSelectedForChecks")]
    [Description("Has commodity been selected for checks?")]
    public bool? IsSelectedForChecks { get; set; }

    [JsonPropertyName("hasChecksComplete")]
    [Description("Has commodity completed this type of check")]
    public bool? HasChecksComplete { get; set; }
}
