using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/format#document-top-level and https://jsonapi.org/ext/atomic/#document-structure.
/// </summary>
public sealed class Document
{
    [JsonPropertyName("links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TopLevelLinks? Links { get; set; }

    [JsonPropertyName("data")]
    // JsonIgnoreCondition is determined at runtime by WriteOnlyDocumentConverter.
    public SingleOrManyData<ResourceObject> Data { get; set; }

    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ErrorObject>? Errors { get; set; }

    [JsonPropertyName("included")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ResourceObject>? Included { get; set; }

    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, object?>? Meta { get; set; }
}
