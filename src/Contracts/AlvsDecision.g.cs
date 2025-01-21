#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record AlvsDecision
{
    [JsonPropertyName("decision")]
    public CustomsClearanceRequest? Decision { get; init; }

    [JsonPropertyName("context")]
    public DecisionContext? Context { get; init; }
}
