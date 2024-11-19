namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ConsignmentCheck
    {
        [JsonPropertyName("euStandard")]
        [Description("Does it conform EU standards")]
        public string[] EuStandard { get; set; }

        [JsonPropertyName("additionalGuarantees")]
        [Description("Result of additional guarantees")]
        public string[] AdditionalGuarantees { get; set; }

        [JsonPropertyName("documentCheckResult")]
        [Description("Result of document check")]
        public string[] DocumentCheckResult { get; set; }

        [JsonPropertyName("nationalRequirements")]
        [Description("Result of national requirements check")]
        public string[] NationalRequirements { get; set; }

        [JsonPropertyName("identityCheckDone")]
        [Description("Was identity check done")]
        public bool IdentityCheckDone { get; set; }

        [JsonPropertyName("identityCheckType")]
        [Description("")]
        public int IdentityCheckType { get; set; }

        [JsonPropertyName("identityCheckResult")]
        [Description("Result of identity check")]
        public string[] IdentityCheckResult { get; set; }

        [JsonPropertyName("identityCheckNotDoneReason")]
        [Description("")]
        public int IdentityCheckNotDoneReason { get; set; }

        [JsonPropertyName("physicalCheckDone")]
        [Description("Was physical check done")]
        public bool PhysicalCheckDone { get; set; }

        [JsonPropertyName("physicalCheckResult")]
        [Description("Result of physical check")]
        public string[] PhysicalCheckResult { get; set; }

        [JsonPropertyName("physicalCheckNotDoneReason")]
        [Description("")]
        public int PhysicalCheckNotDoneReason { get; set; }

        [JsonPropertyName("physicalCheckOtherText")]
        [Description("Other reason to not do physical check")]
        public string[] PhysicalCheckOtherText { get; set; }

        [JsonPropertyName("welfareCheck")]
        [Description("Welfare check")]
        public string[] WelfareCheck { get; set; }

        [JsonPropertyName("numberOfAnimalsChecked")]
        [Description("Number of animals checked")]
        public int NumberOfAnimalsChecked { get; set; }

        [JsonPropertyName("laboratoryCheckDone")]
        [Description("Were laboratory tests done")]
        public bool LaboratoryCheckDone { get; set; }

        [JsonPropertyName("laboratoryCheckResult")]
        [Description("Result of laboratory tests")]
        public string[] LaboratoryCheckResult { get; set; }
    }
}
