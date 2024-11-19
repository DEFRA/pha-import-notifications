namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class LaboratoryTestResult
    {
        [JsonPropertyName("sampleUseByDate")]
        [Description("When sample was used")]
        public string SampleUseByDate { get; set; }

        [JsonPropertyName("releasedOn")]
        [Description("When it was released")]
        public string ReleasedOn { get; set; }

        [JsonPropertyName("laboratoryTestMethod")]
        [Description("Laboratory test method")]
        public string LaboratoryTestMethod { get; set; }

        [JsonPropertyName("results")]
        [Description("Result of test")]
        public string Results { get; set; }

        [JsonPropertyName("conclusion")]
        [Description("")]
        public int Conclusion { get; set; }

        [JsonPropertyName("labTestCreatedOn")]
        [Description("Date of lab test created in IPAFFS")]
        public string LabTestCreatedOn { get; set; }
    }
}
