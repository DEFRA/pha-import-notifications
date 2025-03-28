#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record NotificationResourceResponse
{
    [JsonPropertyName("data")]
    public ImportNotification? Data { get; init; }
}
