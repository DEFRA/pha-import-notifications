#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsClearanceDecision
{
    [JsonPropertyName("serviceHeader")]
    [JsonIgnore]
    public ServiceHeader? ServiceHeader { get; init; }

    [JsonPropertyName("header")]
    public DecisionHeader? Header { get; init; }

    [JsonPropertyName("items")]
    public List<DecisionItems>? Items { get; init; }
}
