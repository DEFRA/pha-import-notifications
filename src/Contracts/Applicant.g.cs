#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class Applicant
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
    [Description("Type of analysis")]
    public ApplicantAnalysisTypeEnum? AnalysisType { get; init; }

    [JsonPropertyName("numberOfSamples")]
    [Description("Number of samples analysed")]
    public int? NumberOfSamples { get; init; }

    [JsonPropertyName("sampleType")]
    [Description("Type of sample")]
    public string? SampleType { get; init; }

    [JsonPropertyName("conservationOfSample")]
    [Description("Conservation of sample")]
    public ApplicantConservationOfSampleEnum? ConservationOfSample { get; init; }

    [JsonPropertyName("inspector")]
    [Description("inspector")]
    public Inspector? Inspector { get; init; }

    [JsonPropertyName("sampledOn")]
    [Description("DateTime")]
    public DateTime? SampledOn { get; init; }
}
