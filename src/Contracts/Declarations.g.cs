#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Declarations
{
    [JsonPropertyName("transits")]
    [Description("A list of declaration ids.")]
    public List<Transits>? Transits { get; init; }

    [JsonPropertyName("customs")]
    [Description("A list of declaration ids.")]
    public List<Customs>? Customs { get; init; }
}
