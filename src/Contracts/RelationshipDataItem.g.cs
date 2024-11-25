#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class RelationshipDataItem
{
    [JsonPropertyName("matched")]
    public bool? Matched { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("links")]
    public ResourceLink Links { get; set; }

    [JsonPropertyName("sourceItem")]
    public int? SourceItem { get; set; }

    [JsonPropertyName("destinationItem")]
    public int? DestinationItem { get; set; }

    [JsonPropertyName("matchingLevel")]
    public int? MatchingLevel { get; set; }
}
