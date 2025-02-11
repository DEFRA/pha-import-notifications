using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Contracts;

public partial record ImportNotification
{
    [JsonPropertyName("clearanceRequests")]
    public IReadOnlyList<CustomsClearanceRequest>? ClearanceRequests { get; init; }

    [JsonPropertyName("clearanceDecisions")]
    public IReadOnlyList<CustomsClearanceDecision>? ClearanceDecisions { get; init; }

    [JsonPropertyName("goodsMovements")]
    public IReadOnlyList<Gmr>? GoodsMovements { get; init; }
}
