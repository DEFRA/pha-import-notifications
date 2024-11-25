#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class TdmRelationshipObject
{
    [JsonPropertyName("matched")]
    public bool? Matched { get; set; }

    [JsonPropertyName("links")]
    public RelationshipLinks Links { get; set; }

    [JsonPropertyName("data")]
    public List<RelationshipDataItem>? Data { get; set; }
}
