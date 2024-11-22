using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class LaboratoryTests
{
    [JsonPropertyName("testedOn")]
    [Description("Date of tests")]
    public DateTime? TestedOn { get; init; }

    [JsonPropertyName("testReason")]
    public required LaboratoryTestsTestReasonEnum TestReason { get; init; }

    [JsonPropertyName("singleLaboratoryTests")]
    [Description("List of details of individual tests performed or to be performed")]
    public List<SingleLaboratoryTest>? SingleLaboratoryTests { get; init; }
}
