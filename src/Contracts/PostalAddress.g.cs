namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PostalAddress
    {
        [JsonPropertyName("addressLine1")]
        [Description("1st line of address")]
        public string AddressLine1 { get; init; }

        [JsonPropertyName("addressLine2")]
        [Description("2nd line of address")]
        public string AddressLine2 { get; init; }

        [JsonPropertyName("addressLine3")]
        [Description("3rd line of address")]
        public string AddressLine3 { get; init; }

        [JsonPropertyName("addressLine4")]
        [Description("4th line of address")]
        public string AddressLine4 { get; init; }

        [JsonPropertyName("county")]
        [Description("3rd line of address")]
        public string County { get; init; }

        [JsonPropertyName("cityOrTown")]
        [Description("City or town name")]
        public string CityOrTown { get; init; }

        [JsonPropertyName("postalCode")]
        [Description("Post code")]
        public string PostalCode { get; init; }
    }
}
