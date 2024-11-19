namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NominatedContact
    {
        [JsonPropertyName("name")]
        [Description("Name of nominated contact")]
        public string Name { get; init; }

        [JsonPropertyName("email")]
        [Description("Email address of nominated contact")]
        public string Email { get; init; }

        [JsonPropertyName("telephone")]
        [Description("Telephone number of nominated contact")]
        public string Telephone { get; init; }
    }
}
