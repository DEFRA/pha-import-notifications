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
        public int TestReason { get; init; }

        [JsonPropertyName("singleLaboratoryTests")]
        [Description("List of details of individual tests performed or to be performed")]
        public Array SingleLaboratoryTests { get; init; }
    }
}
