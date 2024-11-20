namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SealCheck
    {
        [JsonPropertyName("satisfactory")]
        [Description("Is seal check satisfactory")]
        public bool Satisfactory { get; init; }

        [JsonPropertyName("reason")]
        [Description("reason for not satisfactory")]
        public string Reason { get; init; }

        [JsonPropertyName("officialInspector")]
        public OfficialInspector OfficialInspector { get; init; }

        [JsonPropertyName("checkedOn")]
        [Description("date and time of seal check")]
        public DateTime CheckedOn { get; init; }
    }
}
