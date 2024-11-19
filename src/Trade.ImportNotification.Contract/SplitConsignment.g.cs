namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class SplitConsignment
    {
        [JsonPropertyName("validReferenceNumber")]
        [Description("Reference number of the valid split consignment")]
        public string[] ValidReferenceNumber { get; set; }

        [JsonPropertyName("rejectedReferenceNumber")]
        [Description("Reference number of the rejected split consignment")]
        public string[] RejectedReferenceNumber { get; set; }
    }
}
