namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class OfficialVeterinarian
    {
        [JsonPropertyName("firstName")]
        [Description("First name of official veterinarian")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        [Description("Last name of official veterinarian")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Description("Email address of official veterinarian")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        [Description("Phone number of official veterinarian")]
        public string Phone { get; set; }

        [JsonPropertyName("fax")]
        [Description("Fax number of official veterinarian")]
        public string Fax { get; set; }

        [JsonPropertyName("signed")]
        [Description("Date of sign")]
        public string Signed { get; set; }
    }
}
