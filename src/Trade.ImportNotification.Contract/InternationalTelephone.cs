namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class InternationalTelephone
    {
        [JsonPropertyName("countryCode")]
        [Description("Country code of phone number")]
        public string[] CountryCode { get; set; }

        [JsonPropertyName("subscriberNumber")]
        [Description("Phone number")]
        public string[] SubscriberNumber { get; set; }
    }
}