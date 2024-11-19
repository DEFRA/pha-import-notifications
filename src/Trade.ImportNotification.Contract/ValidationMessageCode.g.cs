namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ValidationMessageCode
    {
        [JsonPropertyName("field")]
        [Description("Field")]
        public string Field { get; set; }

        [JsonPropertyName("code")]
        [Description("Code")]
        public string Code { get; set; }
    }
}
