namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Party
    {
        [JsonPropertyName("id")]
        [Description("IPAFFS ID of party")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        [Description("Name of party")]
        public string Name { get; set; }

        [JsonPropertyName("companyId")]
        [Description("Company ID")]
        public string CompanyId { get; set; }

        [JsonPropertyName("contactId")]
        [Description("Contact ID (B2C)")]
        public string ContactId { get; set; }

        [JsonPropertyName("companyName")]
        [Description("Company name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("addresses")]
        [Description("Addresses")]
        public Array Addresses { get; set; }

        [JsonPropertyName("county")]
        [Description("County")]
        public string County { get; set; }

        [JsonPropertyName("postCode")]
        [Description("Post code of party")]
        public string PostCode { get; set; }

        [JsonPropertyName("country")]
        [Description("Country of party")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        [Description("City")]
        public string City { get; set; }

        [JsonPropertyName("tracesId")]
        [Description("TRACES ID")]
        public int TracesId { get; set; }

        [JsonPropertyName("type")]
        [Description("")]
        public int Type { get; set; }

        [JsonPropertyName("approvalNumber")]
        [Description("Approval number")]
        public string ApprovalNumber { get; set; }

        [JsonPropertyName("phone")]
        [Description("Phone number of party")]
        public string Phone { get; set; }

        [JsonPropertyName("fax")]
        [Description("Fax number of party")]
        public string Fax { get; set; }

        [JsonPropertyName("email")]
        [Description("Email number of party")]
        public string Email { get; set; }
    }
}
