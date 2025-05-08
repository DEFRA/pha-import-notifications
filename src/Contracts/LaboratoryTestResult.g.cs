#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record LaboratoryTestResult
{
    [JsonPropertyName("sampleUseByDate")]
    [Description("When sample was used")]
    public string? SampleUseByDate { get; init; }

    [JsonPropertyName("releasedOn")]
    [Description("When it was released")]
    public DateOnly? ReleasedOn { get; init; }

    [JsonPropertyName("laboratoryTestMethod")]
    [Description("Laboratory test method")]
    public string? LaboratoryTestMethod { get; init; }

    [JsonPropertyName("results")]
    [Description("Result of test")]
    public string? Results { get; init; }

    [JsonPropertyName("conclusion")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("NotSatisfactory")]
    [ExampleValue("NotInterpretable")]
    [ExampleValue("Pending")]
    [Description("Conclusion of laboratory test")]
    public string? Conclusion { get; init; }

    [JsonPropertyName("labTestCreatedOn")]
    [Description("Date of lab test created in IPAFFS")]
    public DateOnly? LabTestCreatedOn { get; init; }
}
