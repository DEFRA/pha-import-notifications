#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ClearanceDecisionItem
{
    [JsonPropertyName("itemNumber")]
    public required int ItemNumber { get; init; }

    [JsonPropertyName("checks")]
    public required List<ClearanceDecisionCheck> Checks { get; init; }
}
