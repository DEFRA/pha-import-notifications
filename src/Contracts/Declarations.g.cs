#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Declarations
{
    [JsonPropertyName("transits")]
    public List<Transits>? Transits { get; init; }

    [JsonPropertyName("customs")]
    public List<Customs>? Customs { get; init; }
}
