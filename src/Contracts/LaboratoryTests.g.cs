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
    [ExampleValue("Re-enforced")]
    [ExampleValue("Intensified controls")]
    [ExampleValue("Required")]
    [ExampleValue("Latent infection sampling")]
    [Description("Reason for test. Possible values taken from IPAFFS schema version 17.5.")]
    public string? TestReason { get; init; }

    [JsonPropertyName("singleLaboratoryTests")]
    [Description("List of details of individual tests performed or to be performed")]
    public List<SingleLaboratoryTest>? SingleLaboratoryTests { get; init; }
}
