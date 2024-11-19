namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SyncNotificationsCommand
    {
        [JsonPropertyName("syncPeriod")]
        public int SyncPeriod { get; init; }

        [JsonPropertyName("rootFolder")]
        public string RootFolder { get; init; }

        [JsonPropertyName("jobId")]
        public string JobId { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }
    }
}
