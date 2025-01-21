#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record AlvsDecisionStatus
{
    [JsonPropertyName("decisions")]
    public List<AlvsDecision>? Decisions { get; init; }

    [JsonPropertyName("context")]
    public SummarisedDecisionContext? Context { get; init; }
}
