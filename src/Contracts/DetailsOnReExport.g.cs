#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DetailsOnReExport
{
    [JsonPropertyName("date")]
    [Description("Date of re-export")]
    public DateOnly? Date { get; init; }

    [JsonPropertyName("meansOfTransportNo")]
    [Description("Number of vehicle")]
    public string? MeansOfTransportNo { get; init; }

    [JsonPropertyName("transportType")]
    [ExampleValue("Rail")]
    [ExampleValue("Plane")]
    [ExampleValue("Ship")]
    [ExampleValue("Road")]
    [ExampleValue("Other")]
    [ExampleValue("CShipRoad")]
    [ExampleValue("CShipRail")]
    [Description("Type of transport to be used")]
    public string? TransportType { get; init; }

    [JsonPropertyName("document")]
    [Description("Document issued for re-export")]
    public string? Document { get; init; }

    [JsonPropertyName("countryOfReDispatching")]
    [Description("Two letter ISO code for country of re-dispatching")]
    public string? CountryOfReDispatching { get; init; }

    [JsonPropertyName("exitBip")]
    [Description("Exit BIP (where consignment will leave the country)")]
    public string? ExitBip { get; init; }
}
