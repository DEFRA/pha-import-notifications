namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class OfficialVeterinarian
    {
        [JsonPropertyName("firstName")]
        [Description("First name of official veterinarian")]
        public string FirstName { get; init; }

        [JsonPropertyName("lastName")]
        [Description("Last name of official veterinarian")]
        public string LastName { get; init; }

        [JsonPropertyName("email")]
        [Description("Email address of official veterinarian")]
        public string Email { get; init; }

        [JsonPropertyName("phone")]
        [Description("Phone number of official veterinarian")]
        public string Phone { get; init; }

        [JsonPropertyName("fax")]
        [Description("Fax number of official veterinarian")]
        public string Fax { get; init; }

        [JsonPropertyName("signed")]
        [Description("Date of sign")]
        public string Signed { get; init; }
    }
}
