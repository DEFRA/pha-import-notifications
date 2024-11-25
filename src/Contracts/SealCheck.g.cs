#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class SealCheck
{
    [JsonPropertyName("satisfactory")]
    [Description("Is seal check satisfactory")]
    public bool? Satisfactory { get; set; }

    [JsonPropertyName("reason")]
    [Description("reason for not satisfactory")]
    public string? Reason { get; set; }

    [JsonPropertyName("officialInspector")]
    public OfficialInspector OfficialInspector { get; set; }

    [JsonPropertyName("checkedOn")]
    [Description("date and time of seal check")]
    public DateTime? CheckedOn { get; set; }
}
