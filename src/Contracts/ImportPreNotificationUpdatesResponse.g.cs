#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportPreNotificationUpdatesResponse
{
    [JsonPropertyName("importPreNotificationUpdates")]
    public required List<ImportPreNotificationUpdateResponse> ImportPreNotificationUpdates { get; init; }
}
