#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class MovementTdmRelationships
{
    [JsonPropertyName("notifications")]
    public TdmRelationshipObject? Notifications { get; init; }
}