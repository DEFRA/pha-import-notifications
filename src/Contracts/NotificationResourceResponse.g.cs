namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NotificationResourceResponse
    {
        [JsonPropertyName("data")]
        public ImportNotification Data { get; init; }
    }
}
