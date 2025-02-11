#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record PlannedCrossing
{
    [JsonPropertyName("routeId")]
    [Description("The ID of the crossing route, using a routeId from the GVMS reference data")]
    public string? RouteId { get; init; }

    [JsonPropertyName("departsAt")]
    [Description("The planned date and time of departure, in local time of the departure port. Must not include seconds, time zone or UTC marker")]
    public DateTime? DepartsAt { get; init; }
}
