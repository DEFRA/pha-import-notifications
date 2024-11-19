namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AuditEntry
    {
        [JsonPropertyName("id")]
        [Description("")]
        public string Id { get; set; }

        [JsonPropertyName("version")]
        [Description("")]
        public int Version { get; set; }

        [JsonPropertyName("createdBy")]
        [Description("")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdSource")]
        [Description("")]
        public string CreatedSource { get; set; }

        [JsonPropertyName("createdLocal")]
        [Description("")]
        public string CreatedLocal { get; set; }

        [JsonPropertyName("status")]
        [Description("")]
        public string Status { get; set; }

        [JsonPropertyName("diff")]
        [Description("")]
        public Array Diff { get; set; }
    }
}
