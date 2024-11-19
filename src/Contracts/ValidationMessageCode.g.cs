namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ValidationMessageCode
    {
        [JsonPropertyName("field")]
        [Description("Field")]
        public string Field { get; init; }

        [JsonPropertyName("code")]
        [Description("Code")]
        public string Code { get; init; }
    }
}
