#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportPreNotificationsResponse
{
    [JsonPropertyName("importPreNotifications")]
    public required List<ImportPreNotificationResponse> ImportPreNotifications { get; init; }
}
