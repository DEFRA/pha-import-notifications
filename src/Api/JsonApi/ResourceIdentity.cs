using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// Shared identity information for various JSON:API objects.
/// </summary>
public abstract class ResourceIdentity
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(-3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string? Type { get; set; }

    [JsonPropertyName("id")]
    [JsonPropertyOrder(-2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }
}
