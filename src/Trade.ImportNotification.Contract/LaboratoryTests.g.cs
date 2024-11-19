namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class LaboratoryTests
    {
        [JsonPropertyName("testedOn")]
        [Description("Date of tests")]
        public string TestedOn { get; set; }

        [JsonPropertyName("testReason")]
        [Description("")]
        public int TestReason { get; set; }

        [JsonPropertyName("singleLaboratoryTests")]
        [Description("List of details of individual tests performed or to be performed")]
        public Array SingleLaboratoryTests { get; set; }
    }
}
