namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SyncNotificationsCommand
    {
        [JsonPropertyName("syncPeriod")]
        [Description("")]
        public int SyncPeriod { get; set; }

        [JsonPropertyName("rootFolder")]
        [Description("")]
        public string[] RootFolder { get; set; }

        [JsonPropertyName("jobId")]
        [Description("")]
        public string[] JobId { get; set; }

        [JsonPropertyName("description")]
        [Description("")]
        public string[] Description { get; set; }
    }
}
