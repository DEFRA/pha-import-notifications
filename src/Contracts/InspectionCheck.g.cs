namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class InspectionCheck
    {
        [JsonPropertyName("type")]
        public int Type { get; init; }

        [JsonPropertyName("status")]
        public int Status { get; init; }

        [JsonPropertyName("reason")]
        [Description("Reason for the status if applicable")]
        public string Reason { get; init; }

        [JsonPropertyName("otherReason")]
        [Description("Other reason text when selected reason is 'Other'")]
        public string OtherReason { get; init; }

        [JsonPropertyName("isSelectedForChecks")]
        [Description("Has commodity been selected for checks?")]
        public bool IsSelectedForChecks { get; init; }

        [JsonPropertyName("hasChecksComplete")]
        [Description("Has commodity completed this type of check")]
        public bool HasChecksComplete { get; init; }
    }
}
