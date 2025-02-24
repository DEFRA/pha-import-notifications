#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record GmrRelationships
{
    [JsonPropertyName("importNotifications")]
    public TdmRelationshipObject? ImportNotifications { get; init; }
}
