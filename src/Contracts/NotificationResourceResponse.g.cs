using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class NotificationResourceResponse
{
    [JsonPropertyName("data")]
    public required ImportNotification Data { get; init; }
}
