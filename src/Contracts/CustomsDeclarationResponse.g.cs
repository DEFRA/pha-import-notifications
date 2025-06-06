#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsDeclarationResponse
{
    [JsonPropertyName("movementReferenceNumber")]
    public required string MovementReferenceNumber { get; init; }

    [JsonPropertyName("clearanceRequest")]
    public ClearanceRequest? ClearanceRequest { get; init; }

    [JsonPropertyName("clearanceDecision")]
    public ClearanceDecision? ClearanceDecision { get; init; }

    [JsonPropertyName("finalisation")]
    public Finalisation? Finalisation { get; init; }

    [JsonPropertyName("externalErrors")]
    public List<ExternalError>? ExternalErrors { get; init; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }
}
