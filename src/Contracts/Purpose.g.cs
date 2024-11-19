namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Purpose
    {
        [JsonPropertyName("conformsToEU")]
        [Description("Does consignment conforms to UK laws")]
        public bool ConformsToEU { get; init; }

        [JsonPropertyName("internalMarketPurpose")]
        public int InternalMarketPurpose { get; init; }

        [JsonPropertyName("thirdCountryTranshipment")]
        [Description("Country that consignment is transshipped through")]
        public string ThirdCountryTranshipment { get; init; }

        [JsonPropertyName("forNonConforming")]
        public int ForNonConforming { get; init; }

        [JsonPropertyName("regNumber")]
        [Description("There are 3 types of registration number based on the purpose of consignment. Customs registration number, Free zone registration number and Shipping supplier registration number.")]
        public string RegNumber { get; init; }

        [JsonPropertyName("shipName")]
        [Description("Ship name")]
        public string ShipName { get; init; }

        [JsonPropertyName("shipPort")]
        [Description("Destination Ship port")]
        public string ShipPort { get; init; }

        [JsonPropertyName("exitBip")]
        [Description("Exit Border Inspection Post")]
        public string ExitBip { get; init; }

        [JsonPropertyName("thirdCountry")]
        [Description("Country to which consignment is transited")]
        public string ThirdCountry { get; init; }

        [JsonPropertyName("transitThirdCountries")]
        [Description("Countries that consignment is transited through")]
        public Array TransitThirdCountries { get; init; }

        [JsonPropertyName("forImportOrAdmission")]
        public int ForImportOrAdmission { get; init; }

        [JsonPropertyName("exitDate")]
        [Description("Exit date when import or admission")]
        public string ExitDate { get; init; }

        [JsonPropertyName("finalBip")]
        [Description("Final Border Inspection Post")]
        public string FinalBip { get; init; }

        [JsonPropertyName("purposeGroup")]
        public int PurposeGroup { get; init; }

        [JsonPropertyName("estimatedArrivedAtPortOfExit")]
        [Description("DateTime")]
        public string EstimatedArrivedAtPortOfExit { get; init; }
    }
}
