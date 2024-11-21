namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AuditEntry
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }

        [JsonPropertyName("version")]
        public int Version { get; init; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; init; }

        [JsonPropertyName("createdSource")]
        public DateTime CreatedSource { get; init; }

        [JsonPropertyName("createdLocal")]
        public DateTime CreatedLocal { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; }

        [JsonPropertyName("diff")]
        public List<AuditDiffEntry> Diff { get; init; }
    }
}
