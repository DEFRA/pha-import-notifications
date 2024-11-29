#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class GmrTdmRelationships
{
    [JsonPropertyName("transits")]
    public required TdmRelationshipObject Transits { get; init; }
}
