#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ItemCheck
{
    [JsonPropertyName("itemNumber")]
    public required int ItemNumber { get; init; }

    [JsonPropertyName("checkCode")]
    public string? CheckCode { get; init; }

    [JsonPropertyName("alvsDecisionCode")]
    public string? AlvsDecisionCode { get; init; }

    [JsonPropertyName("btmsDecisionCode")]
    public string? BtmsDecisionCode { get; init; }
}
