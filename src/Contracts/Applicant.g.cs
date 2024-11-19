namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Applicant
    {
        [JsonPropertyName("laboratory")]
        [Description("Name of laboratory")]
        public string Laboratory { get; init; }

        [JsonPropertyName("laboratoryAddress")]
        [Description("Laboratory address")]
        public string LaboratoryAddress { get; init; }

        [JsonPropertyName("laboratoryIdentification")]
        [Description("Laboratory identification")]
        public string LaboratoryIdentification { get; init; }

        [JsonPropertyName("laboratoryPhoneNumber")]
        [Description("Laboratory phone number")]
        public string LaboratoryPhoneNumber { get; init; }

        [JsonPropertyName("laboratoryEmail")]
        [Description("Laboratory email")]
        public string LaboratoryEmail { get; init; }

        [JsonPropertyName("sampleBatchNumber")]
        [Description("Sample batch number")]
        public string SampleBatchNumber { get; init; }

        [JsonPropertyName("analysisType")]
        public int AnalysisType { get; init; }

        [JsonPropertyName("numberOfSamples")]
        [Description("Number of samples analysed")]
        public int NumberOfSamples { get; init; }

        [JsonPropertyName("sampleType")]
        [Description("Type of sample")]
        public string SampleType { get; init; }

        [JsonPropertyName("conservationOfSample")]
        public int ConservationOfSample { get; init; }

        [JsonPropertyName("inspector")]
        public Inspector Inspector { get; init; }

        [JsonPropertyName("sampledOn")]
        [Description("DateTime")]
        public string SampledOn { get; init; }
    }
}
