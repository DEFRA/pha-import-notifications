namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class DetailsOnReExport
    {
        [JsonPropertyName("date")]
        [Description("Date of re-export")]
        public string[] Date { get; set; }

        [JsonPropertyName("meansOfTransportNo")]
        [Description("Number of vehicle")]
        public string[] MeansOfTransportNo { get; set; }

        [JsonPropertyName("transportType")]
        [Description("")]
        public int TransportType { get; set; }

        [JsonPropertyName("document")]
        [Description("Document issued for re-export")]
        public string[] Document { get; set; }

        [JsonPropertyName("countryOfReDispatching")]
        [Description("Two letter ISO code for country of re-dispatching")]
        public string[] CountryOfReDispatching { get; set; }

        [JsonPropertyName("exitBip")]
        [Description("Exit BIP (where consignment will leave the country)")]
        public string[] ExitBip { get; set; }
    }
}
