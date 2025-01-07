#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record SingleLaboratoryTest
{
    [JsonPropertyName("commodityCode")]
    [Description("Commodity code for which lab test was ordered")]
    public string? CommodityCode { get; init; }

    [JsonPropertyName("speciesId")]
    [Description("Species id of commodity for which lab test was ordered")]
    public int? SpeciesId { get; init; }

    [JsonPropertyName("tracesId")]
    [Description("TRACES ID")]
    public int? TracesId { get; init; }

    [JsonPropertyName("testName")]
    [Description("Test name")]
    public string? TestName { get; init; }

    [JsonPropertyName("applicant")]
    [Description("Laboratory tests information details and information about laboratory")]
    public Applicant? Applicant { get; init; }

    [JsonPropertyName("laboratoryTestResult")]
    [Description("Information about results of test")]
    public LaboratoryTestResult? LaboratoryTestResult { get; init; }
}
