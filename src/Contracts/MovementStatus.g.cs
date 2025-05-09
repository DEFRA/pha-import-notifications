#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record MovementStatus
{
    [JsonPropertyName("chedTypes")]
    public List<string>? ChedTypes { get; init; }

    [JsonPropertyName("linkStatus")]
    [ExampleValue("Error")]
    [ExampleValue("PartiallyLinked")]
    [ExampleValue("MissingLinks")]
    [ExampleValue("NoLinks")]
    [ExampleValue("AllLinked")]
    [ExampleValue("Investigate")]
    public required string LinkStatus { get; init; }

    [JsonPropertyName("linkStatusDescription")]
    public string? LinkStatusDescription { get; init; }

    [JsonPropertyName("status")]
    [ExampleValue("DecisionMatch")]
    [ExampleValue("FeatureMissing")]
    [ExampleValue("DataIssue")]
    [ExampleValue("KnownIssue")]
    [ExampleValue("InvestigationNeeded")]
    public string? Status { get; init; }

    [JsonPropertyName("segment")]
    [ExampleValue("Cdms205Ac1")]
    [ExampleValue("Cdms205Ac2")]
    [ExampleValue("Cdms205Ac3")]
    [ExampleValue("Cdms205Ac4")]
    [ExampleValue("Cdms205Ac5")]
    [ExampleValue("Cdms249")]
    [ExampleValue("None")]
    public string? Segment { get; init; }

    [JsonPropertyName("businessDecisionStatus")]
    [ExampleValue("CancelledOrDestroyed")]
    [ExampleValue("ManualReleases")]
    [ExampleValue("AlvsDataErrorDecision")]
    [ExampleValue("BtmsDataErrorDecision")]
    [ExampleValue("MatchComplete")]
    [ExampleValue("MatchGroup")]
    [ExampleValue("AlvsHoldBtmsNotHeld")]
    [ExampleValue("AlvsNotHeldBtmsHold")]
    [ExampleValue("AlvsReleaseBtmsNotReleased")]
    [ExampleValue("AlvsNotReleasedBtmsReleased")]
    [ExampleValue("AlvsRefuseBtmsNotRefused")]
    [ExampleValue("AlvsNotRefusedBtmsRefused")]
    [ExampleValue("AnythingElse")]
    public required string BusinessDecisionStatus { get; init; }
}
