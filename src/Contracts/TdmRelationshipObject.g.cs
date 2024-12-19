#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class TdmRelationshipObject
{
    [JsonPropertyName("matched")]
    public required bool Matched { get; init; }

    [JsonPropertyName("links")]
    public RelationshipLinks? Links { get; init; }

    [JsonPropertyName("data")]
    public List<RelationshipDataItem>? Data { get; init; }
}
