#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class SyncNotificationsCommand
{
    [JsonPropertyName("syncPeriod")]
    public SyncPeriod SyncPeriod { get; set; }

    [JsonPropertyName("rootFolder")]
    public string? RootFolder { get; set; }

    [JsonPropertyName("jobId")]
    public string JobId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
