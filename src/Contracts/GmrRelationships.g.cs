#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record GmrRelationships
{
    [JsonPropertyName("transits")]
    public TdmRelationshipObject? Transits { get; init; }

    [JsonPropertyName("customs")]
    public TdmRelationshipObject? Customs { get; init; }
}
