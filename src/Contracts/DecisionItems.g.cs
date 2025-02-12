#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DecisionItems
{
    [JsonPropertyName("itemNumber")]
    public required int ItemNumber { get; init; }

    [JsonPropertyName("checks")]
    public List<Check>? Checks { get; init; }
}
