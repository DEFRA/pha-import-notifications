#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class MeansOfTransport
{
    [JsonPropertyName("ipaffsType")]
    public required MeansOfTransportTypeEnum IpaffsType { get; init; }

    [JsonPropertyName("document")]
    [Description("Document for transport")]
    public string? Document { get; init; }

    [JsonPropertyName("ipaffsId")]
    [Description("ID of transport")]
    public string? IpaffsId { get; init; }
}
