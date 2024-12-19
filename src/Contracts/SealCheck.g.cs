#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class SealCheck
{
    [JsonPropertyName("satisfactory")]
    [Description("Is seal check satisfactory")]
    public bool? Satisfactory { get; init; }

    [JsonPropertyName("reason")]
    [Description("reason for not satisfactory")]
    public string? Reason { get; init; }

    [JsonPropertyName("officialInspector")]
    [Description("Official inspector")]
    public OfficialInspector? OfficialInspector { get; init; }

    [JsonPropertyName("checkedOn")]
    [Description("date and time of seal check")]
    public DateTime? CheckedOn { get; init; }
}
