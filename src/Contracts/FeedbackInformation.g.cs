#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class FeedbackInformation
{
    [JsonPropertyName("authorityType")]
    public FeedbackInformationAuthorityTypeEnum AuthorityType { get; set; }

    [JsonPropertyName("consignmentArrival")]
    [Description("Did the consignment arrive")]
    public bool? ConsignmentArrival { get; set; }

    [JsonPropertyName("consignmentConformity")]
    [Description("Does the consignment conform")]
    public bool? ConsignmentConformity { get; set; }

    [JsonPropertyName("consignmentNoArrivalReason")]
    [Description("Reason for consignment not arriving at the entry point")]
    public string? ConsignmentNoArrivalReason { get; set; }

    [JsonPropertyName("destructionDate")]
    [Description("Date of consignment destruction")]
    public string? DestructionDate { get; set; }
}
