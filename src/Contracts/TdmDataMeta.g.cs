#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class TdmDataMeta
{
    [JsonPropertyName("matched")]
    public required bool Matched { get; init; }

    [JsonPropertyName("self")]
    public string? Self { get; init; }

    [JsonPropertyName("sourceItem")]
    public int? SourceItem { get; init; }

    [JsonPropertyName("destinationItem")]
    public int? DestinationItem { get; init; }

    [JsonPropertyName("matchingLevel")]
    public int? MatchingLevel { get; init; }
}
