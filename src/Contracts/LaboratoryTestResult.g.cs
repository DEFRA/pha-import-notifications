namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class LaboratoryTestResult
    {
        [JsonPropertyName("sampleUseByDate")]
        [Description("When sample was used")]
        public string SampleUseByDate { get; init; }

        [JsonPropertyName("releasedOn")]
        [Description("When it was released")]
        public string ReleasedOn { get; init; }

        [JsonPropertyName("laboratoryTestMethod")]
        [Description("Laboratory test method")]
        public string LaboratoryTestMethod { get; init; }

        [JsonPropertyName("results")]
        [Description("Result of test")]
        public string Results { get; init; }

        [JsonPropertyName("conclusion")]
        public int Conclusion { get; init; }

        [JsonPropertyName("labTestCreatedOn")]
        [Description("Date of lab test created in IPAFFS")]
        public string LabTestCreatedOn { get; init; }
    }
}
