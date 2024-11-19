namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SealCheck
    {
        [JsonPropertyName("satisfactory")]
        [Description("Is seal check satisfactory")]
        public bool Satisfactory { get; set; }

        [JsonPropertyName("reason")]
        [Description("reason for not satisfactory")]
        public string Reason { get; set; }

        [JsonPropertyName("officialInspector")]
        [Description("")]
        public OfficialInspector OfficialInspector { get; set; }

        [JsonPropertyName("checkedOn")]
        [Description("date and time of seal check")]
        public string CheckedOn { get; set; }
    }
}
