#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class GrmJsonApiTdmRelationshipObject
{
    [JsonPropertyName("links")]
    public required JsonApiRelationshipLinks Links { get; init; }
}
