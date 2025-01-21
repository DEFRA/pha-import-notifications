#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record SummarisedDecisionContext
{
    [JsonPropertyName("alvsDecisionNumber")]
    public int? AlvsDecisionNumber { get; init; }

    [JsonPropertyName("entryVersionNumber")]
    public required int EntryVersionNumber { get; init; }

    [JsonPropertyName("importNotifications")]
    public List<DecisionImportNotifications>? ImportNotifications { get; init; }

    [JsonPropertyName("decisionComparison")]
    public DecisionComparison? DecisionComparison { get; init; }
}
