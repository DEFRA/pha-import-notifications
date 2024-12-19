#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class InspectionOverride
{
    [JsonPropertyName("originalDecision")]
    [Description("Original inspection decision")]
    public string? OriginalDecision { get; init; }

    [JsonPropertyName("overriddenOn")]
    [Description("The time the risk decision is overridden")]
    public DateTime? OverriddenOn { get; init; }

    [JsonPropertyName("overriddenBy")]
    [Description("User entity who has manually overridden the inspection")]
    public UserInformation? OverriddenBy { get; init; }
}
