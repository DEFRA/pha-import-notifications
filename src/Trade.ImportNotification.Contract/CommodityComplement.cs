namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CommodityComplement
    {
        [JsonPropertyName("uniqueComplementId")]
        [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
        public string[] UniqueComplementId { get; set; }

        [JsonPropertyName("commodityDescription")]
        [Description("Description of the commodity")]
        public string[] CommodityDescription { get; set; }

        [JsonPropertyName("commodityId")]
        [Description("The unique commodity ID")]
        public string[] CommodityId { get; set; }

        [JsonPropertyName("complementId")]
        [Description("Identifier of complement chosen from speciesFamily,speciesClass and speciesType'. This is also used to link to the complementParameterSet")]
        public int ComplementId { get; set; }

        [JsonPropertyName("complementName")]
        [Description("Represents the lowest granularity - either type, class, family or species name - for the commodity selected.  This is also used to drive behaviour for EU Import journeys")]
        public string[] ComplementName { get; set; }

        [JsonPropertyName("eppoCode")]
        [Description("EPPO Code related to plant commodities and wood packaging")]
        public string[] EppoCode { get; set; }

        [JsonPropertyName("isWoodPackaging")]
        [Description("(Deprecated in IMTA-11832) Is this commodity wood packaging?")]
        public bool IsWoodPackaging { get; set; }

        [JsonPropertyName("speciesId")]
        [Description("The species ID of the commodity that is imported. Not every commodity has a species ID. This is also used to link to the complementParameterSet. The species ID can change over time")]
        public string[] SpeciesId { get; set; }

        [JsonPropertyName("speciesName")]
        [Description("Species name")]
        public string[] SpeciesName { get; set; }

        [JsonPropertyName("speciesNomination")]
        [Description("Species nomination")]
        public string[] SpeciesNomination { get; set; }

        [JsonPropertyName("speciesTypeName")]
        [Description("Species type name")]
        public string[] SpeciesTypeName { get; set; }

        [JsonPropertyName("speciesType")]
        [Description("Species type identifier of commodity")]
        public string[] SpeciesType { get; set; }

        [JsonPropertyName("speciesClassName")]
        [Description("Species class name")]
        public string[] SpeciesClassName { get; set; }

        [JsonPropertyName("speciesClass")]
        [Description("Species class identifier of commodity")]
        public string[] SpeciesClass { get; set; }

        [JsonPropertyName("speciesFamilyName")]
        [Description("Species family name of commodity")]
        public string[] SpeciesFamilyName { get; set; }

        [JsonPropertyName("speciesFamily")]
        [Description("Species family identifier of commodity")]
        public string[] SpeciesFamily { get; set; }

        [JsonPropertyName("speciesCommonName")]
        [Description("Species common name of commodity for IMP notification simple commodity selection")]
        public string[] SpeciesCommonName { get; set; }

        [JsonPropertyName("isCdsMatched")]
        [Description("Has commodity been matched with corresponding CDS declaration")]
        public bool IsCdsMatched { get; set; }

        [JsonPropertyName("additionalData")]
        [Description("")]
        public object AdditionalData { get; set; }

        [JsonPropertyName("riskAssesment")]
        [Description("")]
        public CommodityRiskResult RiskAssesment { get; set; }

        [JsonPropertyName("checks")]
        [Description("")]
        public Array Checks { get; set; }
    }
}