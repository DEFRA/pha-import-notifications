using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class TdmRelationshipObject
{
    [JsonPropertyName("matched")]
    public bool? Matched { get; init; }

    [JsonPropertyName("links")]
    public required RelationshipLinks Links { get; init; }

    [JsonPropertyName("data")]
    public List<RelationshipDataItem>? Data { get; init; }
}
