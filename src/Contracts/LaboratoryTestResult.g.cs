#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class LaboratoryTestResult
{
    [JsonPropertyName("sampleUseByDate")]
    [Description("When sample was used")]
    public string? SampleUseByDate { get; set; }

    [JsonPropertyName("releasedOn")]
    [Description("When it was released")]
    public DateTime? ReleasedOn { get; set; }

    [JsonPropertyName("laboratoryTestMethod")]
    [Description("Laboratory test method")]
    public string? LaboratoryTestMethod { get; set; }

    [JsonPropertyName("results")]
    [Description("Result of test")]
    public string? Results { get; set; }

    [JsonPropertyName("conclusion")]
    public LaboratoryTestResultConclusionEnum Conclusion { get; set; }

    [JsonPropertyName("labTestCreatedOn")]
    [Description("Date of lab test created in IPAFFS")]
    public DateTime? LabTestCreatedOn { get; set; }
}
