#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class LaboratoryTestResult
{
    [JsonPropertyName("sampleUseByDate")]
    [Description("When sample was used")]
    public string? SampleUseByDate { get; init; }

    [JsonPropertyName("releasedOn")]
    [Description("When it was released")]
    public DateTime? ReleasedOn { get; init; }

    [JsonPropertyName("laboratoryTestMethod")]
    [Description("Laboratory test method")]
    public string? LaboratoryTestMethod { get; init; }

    [JsonPropertyName("results")]
    [Description("Result of test")]
    public string? Results { get; init; }

    [JsonPropertyName("conclusion")]
    public required LaboratoryTestResultConclusionEnum Conclusion { get; init; }

    [JsonPropertyName("labTestCreatedOn")]
    [Description("Date of lab test created in IPAFFS")]
    public DateTime? LabTestCreatedOn { get; init; }
}
