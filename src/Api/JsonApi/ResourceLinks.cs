using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/format/#document-resource-object-links.
/// </summary>
public sealed class ResourceLinks
{
    [JsonPropertyName("self")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Self { get; set; }

    internal bool HasValue()
    {
        return !string.IsNullOrEmpty(Self);
    }
}
