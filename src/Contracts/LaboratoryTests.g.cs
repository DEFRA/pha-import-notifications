#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record LaboratoryTests
{
    [JsonPropertyName("testedOn")]
    [Description("Date of tests")]
    public DateTime? TestedOn { get; init; }

    [JsonPropertyName("testReason")]
    [ExampleValue("Random")]
    [ExampleValue("Suspicious")]
    [ExampleValue("ReEnforced")]
    [ExampleValue("IntensifiedControls")]
    [ExampleValue("Required")]
    [ExampleValue("LatentInfectionSampling")]
    [Description("Reason for test")]
    public string? TestReason { get; init; }

    [JsonPropertyName("singleLaboratoryTests")]
    [Description("List of details of individual tests performed or to be performed")]
    public List<SingleLaboratoryTest>? SingleLaboratoryTests { get; init; }
}
