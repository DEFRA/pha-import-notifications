#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JsonApiRelationshipDataItem
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("meta")]
    public required TdmDataMeta Meta { get; init; }
}
