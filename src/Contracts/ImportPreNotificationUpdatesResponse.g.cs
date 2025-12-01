#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportPreNotificationUpdatesResponse
{
    [JsonPropertyName("importPreNotificationUpdates")]
    public required List<ImportPreNotificationUpdateResponse> ImportPreNotificationUpdates { get; init; }

    [JsonPropertyName("total")]
    public required int Total { get; init; }

    [JsonPropertyName("page")]
    public required int Page { get; init; }

    [JsonPropertyName("pageSize")]
    public required int PageSize { get; init; }
}
