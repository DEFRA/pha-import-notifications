namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class OfficialInspector
    {
        [JsonPropertyName("firstName")]
        [Description("First name of inspector")]
        public string[] FirstName { get; set; }

        [JsonPropertyName("lastName")]
        [Description("Last name of inspector")]
        public string[] LastName { get; set; }

        [JsonPropertyName("email")]
        [Description("Email of inspector")]
        public string[] Email { get; set; }

        [JsonPropertyName("phone")]
        [Description("Phone number of inspector")]
        public string[] Phone { get; set; }

        [JsonPropertyName("fax")]
        [Description("Fax number of inspector")]
        public string[] Fax { get; set; }

        [JsonPropertyName("address")]
        [Description("")]
        public Address Address { get; set; }

        [JsonPropertyName("signed")]
        [Description("Date of sign")]
        public string[] Signed { get; set; }
    }
}