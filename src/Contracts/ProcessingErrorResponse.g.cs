#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ProcessingErrorResponse
{
    [JsonPropertyName("movementReferenceNumber")]
    public required string MovementReferenceNumber { get; init; }

    [JsonPropertyName("processingError")]
    public required ProcessingError ProcessingError { get; init; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }
}
