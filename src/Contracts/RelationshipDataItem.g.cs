#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record RelationshipDataItem
{
    [JsonPropertyName("matched")]
    public bool? Matched { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("links")]
    public ResourceLink? Links { get; init; }

    [JsonPropertyName("sourceItem")]
    public int? SourceItem { get; init; }

    [JsonPropertyName("destinationItem")]
    public int? DestinationItem { get; init; }

    [JsonPropertyName("matchingLevel")]
    public int? MatchingLevel { get; init; }
}
