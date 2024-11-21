namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class LaboratoryTests
    {
        [JsonPropertyName("testedOn")]
        [Description("Date of tests")]
        public DateTime TestedOn { get; init; }

        [JsonPropertyName("testReason")]
        public LaboratoryTestsTestReasonEnum TestReason { get; init; }

        [JsonPropertyName("singleLaboratoryTests")]
        [Description("List of details of individual tests performed or to be performed")]
        public List<SingleLaboratoryTest> SingleLaboratoryTests { get; init; }
    }
}
