#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Applicant
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
    [ExampleValue("Initial analysis")]
    [ExampleValue("Counter analysis")]
    [ExampleValue("Second expert analysis")]
    [Description("Type of analysis. Possible values taken from IPAFFS schema version 17.5.")]
    public string? AnalysisType { get; init; }

    [JsonPropertyName("numberOfSamples")]
    [Description("Number of samples analysed")]
    public int? NumberOfSamples { get; init; }

    [JsonPropertyName("sampleType")]
    [Description("Type of sample")]
    public string? SampleType { get; init; }

    [JsonPropertyName("conservationOfSample")]
    [ExampleValue("Ambient")]
    [ExampleValue("Chilled")]
    [ExampleValue("Frozen")]
    [Description("Conservation of sample. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ConservationOfSample { get; init; }

    [JsonPropertyName("inspector")]
    [Description("inspector")]
    public Inspector? Inspector { get; init; }

    [JsonPropertyName("sampledOn")]
    [Description("DateTime")]
    public DateTime? SampledOn { get; init; }
}
