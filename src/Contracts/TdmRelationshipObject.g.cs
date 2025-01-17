#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record TdmRelationshipObject
{
    [JsonPropertyName("links")]
    public RelationshipLinks? Links { get; init; }

    [JsonPropertyName("data")]
    public List<RelationshipDataItem>? Data { get; init; }
}
