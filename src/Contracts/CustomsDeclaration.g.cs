#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsDeclaration
{
    [JsonPropertyName("clearanceRequest")]
    public ClearanceRequest? ClearanceRequest { get; init; }

    [JsonPropertyName("clearanceDecision")]
    public ClearanceDecision? ClearanceDecision { get; init; }

    [JsonPropertyName("finalisation")]
    public Finalisation? Finalisation { get; init; }

    [JsonPropertyName("inboundError")]
    public InboundError? InboundError { get; init; }
}
