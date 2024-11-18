namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class BillingInformation
    {
        [JsonPropertyName("isConfirmed")]
        [Description("Indicates whether user has confirmed their billing information")]
        public bool IsConfirmed { get; set; }

        [JsonPropertyName("emailAddress")]
        [Description("Billing email address")]
        public string[] EmailAddress { get; set; }

        [JsonPropertyName("phoneNumber")]
        [Description("Billing phone number")]
        public string[] PhoneNumber { get; set; }

        [JsonPropertyName("contactName")]
        [Description("Billing Contact Name")]
        public string[] ContactName { get; set; }

        [JsonPropertyName("postalAddress")]
        [Description("")]
        public PostalAddress PostalAddress { get; set; }
    }
}