#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class JsonApiTdmRelationshipMeta
{
    [JsonPropertyName("matched")]
    public required bool Matched { get; init; }
}
