namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NotificationResourceResponse
    {
        [JsonPropertyName("data")]
        [Description("")]
        public ImportNotification Data { get; set; }
    }
}
