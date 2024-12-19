#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class ServiceHeader
{
    [JsonPropertyName("sourceSystem")]
    public string? SourceSystem { get; init; }

    [JsonPropertyName("destinationSystem")]
    public string? DestinationSystem { get; init; }

    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("serviceCalled")]
    public DateTime? ServiceCalled { get; init; }
}
