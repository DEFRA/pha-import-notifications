#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class MeansOfTransport
{
    [JsonPropertyName("type")]
    public MeansOfTransportTypeEnum Type { get; set; }

    [JsonPropertyName("document")]
    [Description("Document for transport")]
    public string? Document { get; set; }

    [JsonPropertyName("id")]
    [Description("ID of transport")]
    public string? Id { get; set; }
}
