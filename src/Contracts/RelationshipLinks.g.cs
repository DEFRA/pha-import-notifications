using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class RelationshipLinks
{
    [JsonPropertyName("self")]
    public string? Self { get; init; }

    [JsonPropertyName("related")]
    public string? Related { get; init; }
}
