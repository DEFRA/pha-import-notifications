namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AuditDiffEntry
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("op")]
        public string Op { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}
