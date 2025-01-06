#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record MeansOfTransport
{
    [JsonPropertyName("type")]
    [Description("Type of transport")]
    public MeansOfTransportTypeEnum? Type { get; init; }

    [JsonPropertyName("document")]
    [Description("Document for transport")]
    public string? Document { get; init; }

    [JsonPropertyName("id")]
    [Description("ID of transport")]
    public string? Id { get; init; }
}
