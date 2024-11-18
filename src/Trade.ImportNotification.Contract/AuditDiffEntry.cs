namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AuditDiffEntry
    {
        [JsonPropertyName("path")]
        [Description("")]
        public string[] Path { get; set; }

        [JsonPropertyName("op")]
        [Description("")]
        public string[] Op { get; set; }

        [JsonPropertyName("value")]
        [Description("")]
        public object Value { get; set; }
    }
}
