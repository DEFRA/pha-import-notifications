namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class FeedbackInformation
    {
        [JsonPropertyName("authorityType")]
        public int AuthorityType { get; init; }

        [JsonPropertyName("consignmentArrival")]
        [Description("Did the consignment arrive")]
        public bool ConsignmentArrival { get; init; }

        [JsonPropertyName("consignmentConformity")]
        [Description("Does the consignment conform")]
        public bool ConsignmentConformity { get; init; }

        [JsonPropertyName("consignmentNoArrivalReason")]
        [Description("Reason for consignment not arriving at the entry point")]
        public string ConsignmentNoArrivalReason { get; init; }

        [JsonPropertyName("destructionDate")]
        [Description("Date of consignment destruction")]
        public string DestructionDate { get; init; }
    }
}
