#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JsonApiTdmRelationshipObject
{
    [JsonPropertyName("meta")]
    public required JsonApiTdmRelationshipMeta Meta { get; init; }

    [JsonPropertyName("links")]
    public required JsonApiRelationshipLinks Links { get; init; }

    [JsonPropertyName("data")]
    public List<JsonApiRelationshipDataItem>? Data { get; init; }
}
