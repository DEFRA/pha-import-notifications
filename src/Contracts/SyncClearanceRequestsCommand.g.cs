namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SyncClearanceRequestsCommand
    {
        [JsonPropertyName("syncPeriod")]
        public int SyncPeriod { get; set; }

        [JsonPropertyName("rootFolder")]
        public string RootFolder { get; set; }

        [JsonPropertyName("jobId")]
        public string JobId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
