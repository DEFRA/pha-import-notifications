#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record MeansOfTransport
{
    [JsonPropertyName("type")]
    [ExampleValue("Aeroplane")]
    [ExampleValue("Road Vehicle")]
    [ExampleValue("Railway Wagon")]
    [ExampleValue("Ship")]
    [ExampleValue("Other")]
    [ExampleValue("Road vehicle Aeroplane")]
    [ExampleValue("Ship Railway wagon")]
    [ExampleValue("Ship Road vehicle")]
    [Description("Type of transport. Possible values taken from IPAFFS schema version 17.5.")]
    public string? Type { get; init; }

    [JsonPropertyName("document")]
    [Description("Document for transport")]
    public string? Document { get; init; }

    [JsonPropertyName("id")]
    [Description("ID of transport")]
    public string? Id { get; init; }
}
