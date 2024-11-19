namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class InspectionCheck
    {
        [JsonPropertyName("type")]
        [Description("")]
        public int Type { get; set; }

        [JsonPropertyName("status")]
        [Description("")]
        public int Status { get; set; }

        [JsonPropertyName("reason")]
        [Description("Reason for the status if applicable")]
        public string Reason { get; set; }

        [JsonPropertyName("otherReason")]
        [Description("Other reason text when selected reason is 'Other'")]
        public string OtherReason { get; set; }

        [JsonPropertyName("isSelectedForChecks")]
        [Description("Has commodity been selected for checks?")]
        public bool IsSelectedForChecks { get; set; }

        [JsonPropertyName("hasChecksComplete")]
        [Description("Has commodity completed this type of check")]
        public bool HasChecksComplete { get; set; }
    }
}
