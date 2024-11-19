namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Commodities
    {
        [JsonPropertyName("gmsDeclarationAccepted")]
        [Description("Flag to record when the GMS declaration has been accepted")]
        public bool GmsDeclarationAccepted { get; set; }

        [JsonPropertyName("consignedCountryInChargeGroup")]
        [Description("Flag to record whether the consigned country is in an ipaffs charge group")]
        public bool ConsignedCountryInChargeGroup { get; set; }

        [JsonPropertyName("totalGrossWeight")]
        [Description("The total gross weight of the consignment.  It must be bigger than the total net weight of the commodities")]
        public decimal TotalGrossWeight { get; set; }

        [JsonPropertyName("totalNetWeight")]
        [Description("The total net weight of the commodities within this consignment")]
        public decimal TotalNetWeight { get; set; }

        [JsonPropertyName("totalGrossVolume")]
        [Description("The total gross volume of the commodities within this consignment")]
        public decimal TotalGrossVolume { get; set; }

        [JsonPropertyName("totalGrossVolumeUnit")]
        [Description("Unit used for specifying total gross volume of this consignment (litres or metres cubed)")]
        public string TotalGrossVolumeUnit { get; set; }

        [JsonPropertyName("numberOfPackages")]
        [Description("The total number of packages within this consignment")]
        public int NumberOfPackages { get; set; }

        [JsonPropertyName("temperature")]
        [Description("Temperature (type) of commodity")]
        public string Temperature { get; set; }

        [JsonPropertyName("numberOfAnimals")]
        [Description("The total number of animals within this consignment")]
        public int NumberOfAnimals { get; set; }

        [JsonPropertyName("includeNonAblactedAnimals")]
        [Description("Does consignment contain ablacted animals")]
        public bool IncludeNonAblactedAnimals { get; set; }

        [JsonPropertyName("countryOfOrigin")]
        [Description("Consignments country of origin")]
        public string CountryOfOrigin { get; set; }

        [JsonPropertyName("countryOfOriginIsPodCountry")]
        [Description("Flag to record whether country of origin is a temporary PoD country")]
        public bool CountryOfOriginIsPodCountry { get; set; }

        [JsonPropertyName("isLowRiskArticle72Country")]
        [Description("Flag to record whether country of origin is a low risk article 72 country")]
        public bool IsLowRiskArticle72Country { get; set; }

        [JsonPropertyName("regionOfOrigin")]
        [Description("Region of country")]
        public string RegionOfOrigin { get; set; }

        [JsonPropertyName("consignedCountry")]
        [Description("Country from where commodity was sent")]
        public string ConsignedCountry { get; set; }

        [JsonPropertyName("animalsCertifiedAs")]
        [Description("Certification of animals (Breeding, slaughter etc.)")]
        public string AnimalsCertifiedAs { get; set; }

        [JsonPropertyName("commodityIntendedFor")]
        public int CommodityIntendedFor { get; set; }
    }
}
