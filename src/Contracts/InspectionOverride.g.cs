#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class InspectionOverride
{
    [JsonPropertyName("originalDecision")]
    [Description("Original inspection decision")]
    public string? OriginalDecision { get; init; }

    [JsonPropertyName("overriddenOn")]
    [Description("The time the risk decision is overridden")]
    public DateTime? OverriddenOn { get; init; }

    [JsonPropertyName("overriddenBy")]
    public required UserInformation OverriddenBy { get; init; }
}
