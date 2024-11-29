using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/format/#document-resource-object-relationships.
/// </summary>
public sealed class RelationshipObject
{
    [JsonPropertyName("links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RelationshipLinks? Links { get; set; }

    [JsonPropertyName("data")]
    // JsonIgnoreCondition is determined at runtime by WriteOnlyRelationshipObjectConverter.
    public SingleOrManyData<ResourceIdentifierObject> Data { get; set; }
}
