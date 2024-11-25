#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class SealContainer
{
    [JsonPropertyName("sealNumber")]
    public string? SealNumber { get; set; }

    [JsonPropertyName("containerNumber")]
    public string? ContainerNumber { get; set; }

    [JsonPropertyName("officialSeal")]
    public bool? OfficialSeal { get; set; }

    [JsonPropertyName("resealedSealNumber")]
    public string? ResealedSealNumber { get; set; }
}
