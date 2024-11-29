#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class SealCheck
{
    [JsonPropertyName("satisfactory")]
    [Description("Is seal check satisfactory")]
    public bool? Satisfactory { get; init; }

    [JsonPropertyName("reason")]
    [Description("reason for not satisfactory")]
    public string? Reason { get; init; }

    [JsonPropertyName("officialInspector")]
    public required OfficialInspector OfficialInspector { get; init; }

    [JsonPropertyName("dateTimeOfCheck")]
    [Description("date and time of seal check")]
    public DateTime? DateTimeOfCheck { get; init; }
}
