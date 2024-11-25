#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class InspectionOverride
{
    [JsonPropertyName("originalDecision")]
    [Description("Original inspection decision")]
    public string? OriginalDecision { get; set; }

    [JsonPropertyName("overriddenOn")]
    [Description("The time the risk decision is overridden")]
    public DateTime? OverriddenOn { get; set; }

    [JsonPropertyName("overriddenBy")]
    public UserInformation OverriddenBy { get; set; }
}
