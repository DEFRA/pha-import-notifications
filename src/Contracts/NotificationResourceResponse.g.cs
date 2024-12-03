#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class NotificationResourceResponse
{
    [JsonPropertyName("data")]
    public ImportNotification? Data { get; init; }
}
