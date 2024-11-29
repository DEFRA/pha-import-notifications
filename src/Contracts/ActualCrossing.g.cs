#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ActualCrossing
{
    [JsonPropertyName("routeId")]
    public string? RouteId { get; init; }

    [JsonPropertyName("arrivesAt")]
    public DateTime? ArrivesAt { get; init; }
}
