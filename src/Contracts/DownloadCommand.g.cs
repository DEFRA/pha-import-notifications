#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DownloadCommand
{
    [JsonPropertyName("syncPeriod")]
    public required SyncPeriod SyncPeriod { get; init; }

    [JsonPropertyName("jobId")]
    public required string JobId { get; init; }

    [JsonPropertyName("timespan")]
    public string? Timespan { get; init; }

    [JsonPropertyName("resource")]
    public string? Resource { get; init; }

    [JsonPropertyName("filter")]
    public DownloadFilter? Filter { get; init; }

    [JsonPropertyName("rootFolder")]
    public string? RootFolder { get; init; }
}
