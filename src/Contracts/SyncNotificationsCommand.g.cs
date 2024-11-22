using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class SyncNotificationsCommand
{
    [JsonPropertyName("syncPeriod")]
    public required SyncPeriod SyncPeriod { get; init; }

    [JsonPropertyName("rootFolder")]
    public string? RootFolder { get; init; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
