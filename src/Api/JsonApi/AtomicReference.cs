using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See "ref" in https://jsonapi.org/ext/atomic/#operation-objects.
/// </summary>
public sealed class AtomicReference : ResourceIdentity
{
    [JsonPropertyName("relationship")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Relationship { get; set; }
}
