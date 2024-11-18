namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class PostalAddress
    {
        [JsonPropertyName("addressLine1")]
        [Description("1st line of address")]
        public string[] AddressLine1 { get; set; }

        [JsonPropertyName("addressLine2")]
        [Description("2nd line of address")]
        public string[] AddressLine2 { get; set; }

        [JsonPropertyName("addressLine3")]
        [Description("3rd line of address")]
        public string[] AddressLine3 { get; set; }

        [JsonPropertyName("addressLine4")]
        [Description("4th line of address")]
        public string[] AddressLine4 { get; set; }

        [JsonPropertyName("county")]
        [Description("3rd line of address")]
        public string[] County { get; set; }

        [JsonPropertyName("cityOrTown")]
        [Description("City or town name")]
        public string[] CityOrTown { get; set; }

        [JsonPropertyName("postalCode")]
        [Description("Post code")]
        public string[] PostalCode { get; set; }
    }
}