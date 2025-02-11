#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CheckedInCrossing
{
    [JsonPropertyName("routeId")]
    [Description("The ID of the crossing route, using a routeId from the GVMS reference data")]
    public string? RouteId { get; init; }

    [JsonPropertyName("arrivesAt")]
    [Description("The planned date and time of arrival, in local time of the arrival port. Must not include seconds, time zone or UTC marker")]
    public DateTime? ArrivesAt { get; init; }
}
