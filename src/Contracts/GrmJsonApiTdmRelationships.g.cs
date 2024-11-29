#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class GrmJsonApiTdmRelationships
{
    [JsonPropertyName("notifications")]
    public required GrmJsonApiTdmRelationshipObject Notifications { get; init; }

    [JsonPropertyName("movements")]
    public required GrmJsonApiTdmRelationshipObject Movements { get; init; }
}
