using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/ext/atomic/#result-objects.
/// </summary>
public sealed class AtomicResultObject
{
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SingleOrManyData<ResourceObject> Data { get; set; }

    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IDictionary<string, object?>? Meta { get; set; }
}
