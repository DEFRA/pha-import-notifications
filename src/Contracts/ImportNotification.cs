using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Contracts;

public partial record ImportPreNotification()
{
    [JsonPropertyName("clearanceRequests")]
    public IList<CustomsDeclarationData>? CustomsDeclarations { get; set; }

    [JsonPropertyName("goodsMovements")]
    public IList<Gmr>? GoodsMovements { get; set; }
}

public record CustomsDeclarationData()
{
    [JsonPropertyName("movementReferenceNumber")]
    public string? MovementReferenceNumber { get; init; }

    [JsonPropertyName("clearanceRequest")]
    public ClearanceRequest? ClearanceRequest { get; init; }

    [JsonPropertyName("clearanceDecision")]
    public ClearanceDecision? ClearanceDecision { get; init; }

    [JsonPropertyName("finalisation")]
    public Finalisation? Finalisation { get; init; }
}
