namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Applicant
    {
        [JsonPropertyName("laboratory")]
        [Description("Name of laboratory")]
        public string[] Laboratory { get; set; }

        [JsonPropertyName("laboratoryAddress")]
        [Description("Laboratory address")]
        public string[] LaboratoryAddress { get; set; }

        [JsonPropertyName("laboratoryIdentification")]
        [Description("Laboratory identification")]
        public string[] LaboratoryIdentification { get; set; }

        [JsonPropertyName("laboratoryPhoneNumber")]
        [Description("Laboratory phone number")]
        public string[] LaboratoryPhoneNumber { get; set; }

        [JsonPropertyName("laboratoryEmail")]
        [Description("Laboratory email")]
        public string[] LaboratoryEmail { get; set; }

        [JsonPropertyName("sampleBatchNumber")]
        [Description("Sample batch number")]
        public string[] SampleBatchNumber { get; set; }

        [JsonPropertyName("analysisType")]
        [Description("")]
        public int AnalysisType { get; set; }

        [JsonPropertyName("numberOfSamples")]
        [Description("Number of samples analysed")]
        public int NumberOfSamples { get; set; }

        [JsonPropertyName("sampleType")]
        [Description("Type of sample")]
        public string[] SampleType { get; set; }

        [JsonPropertyName("conservationOfSample")]
        [Description("")]
        public int ConservationOfSample { get; set; }

        [JsonPropertyName("inspector")]
        [Description("")]
        public Inspector Inspector { get; set; }

        [JsonPropertyName("sampledOn")]
        [Description("DateTime")]
        public string[] SampledOn { get; set; }
    }
}
