#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class LaboratoryTests
{
    [JsonPropertyName("testedOn")]
    [Description("Date of tests")]
    public DateTime? TestedOn { get; set; }

    [JsonPropertyName("testReason")]
    public LaboratoryTestsTestReasonEnum TestReason { get; set; }

    [JsonPropertyName("singleLaboratoryTests")]
    [Description("List of details of individual tests performed or to be performed")]
    public List<SingleLaboratoryTest>? SingleLaboratoryTests { get; set; }
}
