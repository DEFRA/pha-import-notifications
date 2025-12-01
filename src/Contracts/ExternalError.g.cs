#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ExternalError
{
    [JsonPropertyName("externalCorrelationId")]
    public string? ExternalCorrelationId { get; init; }

    [JsonPropertyName("sourceCorrelationId")]
    public string? SourceCorrelationId { get; init; }

    [JsonPropertyName("externalVersion")]
    public int? ExternalVersion { get; init; }

    [JsonPropertyName("messageSentAt")]
    public required DateTime MessageSentAt { get; init; }

    [JsonPropertyName("errors")]
    public List<ErrorItem>? Errors { get; init; }
}
