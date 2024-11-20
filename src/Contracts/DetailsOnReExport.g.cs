namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class DetailsOnReExport
    {
        [JsonPropertyName("date")]
        [Description("Date of re-export")]
        public DateTime Date { get; init; }

        [JsonPropertyName("meansOfTransportNo")]
        [Description("Number of vehicle")]
        public string MeansOfTransportNo { get; init; }

        [JsonPropertyName("transportType")]
        public DetailsOnReExportTransportTypeEnum TransportType { get; init; }

        [JsonPropertyName("document")]
        [Description("Document issued for re-export")]
        public string Document { get; init; }

        [JsonPropertyName("countryOfReDispatching")]
        [Description("Two letter ISO code for country of re-dispatching")]
        public string CountryOfReDispatching { get; init; }

        [JsonPropertyName("exitBip")]
        [Description("Exit BIP (where consignment will leave the country)")]
        public string ExitBip { get; init; }
    }
}
