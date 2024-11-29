#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Applicant
{
    [JsonPropertyName("laboratory")]
    [Description("Name of laboratory")]
    public string? Laboratory { get; init; }

    [JsonPropertyName("laboratoryAddress")]
    [Description("Laboratory address")]
    public string? LaboratoryAddress { get; init; }

    [JsonPropertyName("laboratoryIdentification")]
    [Description("Laboratory identification")]
    public string? LaboratoryIdentification { get; init; }

    [JsonPropertyName("laboratoryPhoneNumber")]
    [Description("Laboratory phone number")]
    public string? LaboratoryPhoneNumber { get; init; }

    [JsonPropertyName("laboratoryEmail")]
    [Description("Laboratory email")]
    public string? LaboratoryEmail { get; init; }

    [JsonPropertyName("sampleBatchNumber")]
    [Description("Sample batch number")]
    public string? SampleBatchNumber { get; init; }

    [JsonPropertyName("analysisType")]
    public required ApplicantAnalysisTypeEnum AnalysisType { get; init; }

    [JsonPropertyName("numberOfSamples")]
    [Description("Number of samples analysed")]
    public int? NumberOfSamples { get; init; }

    [JsonPropertyName("sampleType")]
    [Description("Type of sample")]
    public string? SampleType { get; init; }

    [JsonPropertyName("conservationOfSample")]
    public required ApplicantConservationOfSampleEnum ConservationOfSample { get; init; }

    [JsonPropertyName("inspector")]
    public required Inspector Inspector { get; init; }

    [JsonPropertyName("sampleDate")]
    [Description("Date the sample is taken")]
    public string? SampleDate { get; init; }

    [JsonPropertyName("sampleTime")]
    [Description("Time the sample is taken")]
    public string? SampleTime { get; init; }
}
