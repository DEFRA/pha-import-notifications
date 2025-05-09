#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record MeansOfTransport
{
    [JsonPropertyName("type")]
    [ExampleValue("Aeroplane")]
    [ExampleValue("RoadVehicle")]
    [ExampleValue("RailwayWagon")]
    [ExampleValue("Ship")]
    [ExampleValue("Other")]
    [ExampleValue("RoadVehicleAeroplane")]
    [ExampleValue("ShipRailwayWagon")]
    [ExampleValue("ShipRoadVehicle")]
    [Description("Type of transport")]
    public string? Type { get; init; }

    [JsonPropertyName("document")]
    [Description("Document for transport")]
    public string? Document { get; init; }

    [JsonPropertyName("id")]
    [Description("ID of transport")]
    public string? Id { get; init; }
}
