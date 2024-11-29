using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See "links" in https://jsonapi.org/format/#document-resource-object-relationships.
/// </summary>
public sealed class RelationshipLinks
{
    [JsonPropertyName("self")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Self { get; set; }

    [JsonPropertyName("related")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Related { get; set; }
}
