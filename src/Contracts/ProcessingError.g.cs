#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ProcessingError
{
    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("sourceExternalCorrelationId")]
    public string? SourceExternalCorrelationId { get; init; }

    [JsonPropertyName("externalVersion")]
    public int? ExternalVersion { get; init; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; init; }

    [JsonPropertyName("errors")]
    public required List<ErrorItem> Errors { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
