#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record FeedbackInformation
{
    [JsonPropertyName("authorityType")]
    [ExampleValue("exitbip")]
    [ExampleValue("finalbip")]
    [ExampleValue("localvetunit")]
    [ExampleValue("inspunit")]
    [Description("Type of authority. Possible values taken from IPAFFS schema version 17.5.")]
    public string? AuthorityType { get; init; }

    [JsonPropertyName("consignmentArrival")]
    [Description("Did the consignment arrive")]
    public bool? ConsignmentArrival { get; init; }

    [JsonPropertyName("consignmentConformity")]
    [Description("Does the consignment conform")]
    public bool? ConsignmentConformity { get; init; }

    [JsonPropertyName("consignmentNoArrivalReason")]
    [Description("Reason for consignment not arriving at the entry point")]
    public string? ConsignmentNoArrivalReason { get; init; }

    [JsonPropertyName("destructionDate")]
    [Description("Date of consignment destruction")]
    public string? DestructionDate { get; init; }
}
