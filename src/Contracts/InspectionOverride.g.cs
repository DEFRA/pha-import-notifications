namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class InspectionOverride
    {
        [JsonPropertyName("originalDecision")]
        [Description("Original inspection decision")]
        public string OriginalDecision { get; set; }

        [JsonPropertyName("overriddenOn")]
        [Description("The time the risk decision is overridden")]
        public string OverriddenOn { get; set; }

        [JsonPropertyName("overriddenBy")]
        public UserInformation OverriddenBy { get; set; }
    }
}
