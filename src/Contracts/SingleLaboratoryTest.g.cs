namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SingleLaboratoryTest
    {
        [JsonPropertyName("commodityCode")]
        [Description("Commodity code for which lab test was ordered")]
        public string CommodityCode { get; set; }

        [JsonPropertyName("speciesId")]
        [Description("Species id of commodity for which lab test was ordered")]
        public int SpeciesId { get; set; }

        [JsonPropertyName("tracesId")]
        [Description("TRACES ID")]
        public int TracesId { get; set; }

        [JsonPropertyName("testName")]
        [Description("Test name")]
        public string TestName { get; set; }

        [JsonPropertyName("applicant")]
        [Description("")]
        public Applicant Applicant { get; set; }

        [JsonPropertyName("laboratoryTestResult")]
        [Description("")]
        public LaboratoryTestResult LaboratoryTestResult { get; set; }
    }
}
