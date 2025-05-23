using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/format/#document-resource-objects.
/// </summary>
public sealed class ResourceObject : ResourceIdentifierObject
{
    [JsonPropertyName("attributes")]
    [JsonPropertyOrder(1)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, object?> Attributes { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("relationships")]
    [JsonPropertyOrder(2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, RelationshipObject?>? Relationships { get; set; }

    [JsonPropertyName("links")]
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResourceLinks? Links { get; set; }
}
