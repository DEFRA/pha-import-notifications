namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ApprovedEstablishment
    {
        [JsonPropertyName("id")]
        [Description("ID")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        [Description("Name of approved establishment")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        [Description("Country of approved establishment")]
        public string Country { get; set; }

        [JsonPropertyName("types")]
        [Description("Types of approved establishment")]
        public Array Types { get; set; }

        [JsonPropertyName("approvalNumber")]
        [Description("Approval number")]
        public string ApprovalNumber { get; set; }

        [JsonPropertyName("section")]
        [Description("Section of approved establishment")]
        public string Section { get; set; }
    }
}
