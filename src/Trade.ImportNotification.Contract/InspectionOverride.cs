namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class InspectionOverride
    {
        [JsonPropertyName("originalDecision")]
        [Description("Original inspection decision")]
        public string[] OriginalDecision { get; set; }

        [JsonPropertyName("overriddenOn")]
        [Description("The time the risk decision is overridden")]
        public string[] OverriddenOn { get; set; }

        [JsonPropertyName("overriddenBy")]
        [Description("")]
        public UserInformation OverriddenBy { get; set; }
    }
}
