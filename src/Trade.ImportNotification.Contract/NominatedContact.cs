namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NominatedContact
    {
        [JsonPropertyName("name")]
        [Description("Name of nominated contact")]
        public string[] Name { get; set; }

        [JsonPropertyName("email")]
        [Description("Email address of nominated contact")]
        public string[] Email { get; set; }

        [JsonPropertyName("telephone")]
        [Description("Telephone number of nominated contact")]
        public string[] Telephone { get; set; }
    }
}
