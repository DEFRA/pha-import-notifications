#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DecisionComparison
{
    [JsonPropertyName("paired")]
    public required bool Paired { get; init; }

    [JsonPropertyName("decisionStatus")]
    [ExampleValue("BtmsMadeSameDecisionAsAlvs")]
    [ExampleValue("BtmMadeSameDecisionTypeAsAlvs")]
    [ExampleValue("NoImportNotificationsLinked")]
    [ExampleValue("PartialImportNotificationsLinked")]
    [ExampleValue("NoAlvsDecisions")]
    [ExampleValue("DocumentReferenceFormatIncorrect")]
    [ExampleValue("DocumentReferenceCaseIncorrect")]
    [ExampleValue("AlvsX00NotBtms")]
    [ExampleValue("ReliesOnCDMS205")]
    [ExampleValue("ReliesOnCDMS249")]
    [ExampleValue("HasChedppChecks")]
    [ExampleValue("HasOtherDataErrors")]
    [ExampleValue("HasGenericDataErrors")]
    [ExampleValue("HasMultipleChedTypes")]
    [ExampleValue("HasMultipleCheds")]
    [ExampleValue("BtmsClearAlvsHold")]
    [ExampleValue("AlvsClearBtmsHold")]
    [ExampleValue("InvestigationNeeded")]
    [ExampleValue("None")]
    public required string DecisionStatus { get; init; }

    [JsonPropertyName("decisionMatched")]
    public required bool DecisionMatched { get; init; }

    [JsonPropertyName("btmsDecisionNumber")]
    public int? BtmsDecisionNumber { get; init; }

    [JsonPropertyName("checks")]
    public List<ItemCheck>? Checks { get; init; }
}
