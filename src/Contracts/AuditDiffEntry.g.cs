namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AuditDiffEntry
    {
        [JsonPropertyName("path")]
        public string Path { get; init; }

        [JsonPropertyName("op")]
        public string Op { get; init; }

        [JsonPropertyName("value")]
        public object Value { get; init; }
    }
}
