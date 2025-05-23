#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Control
{
    [JsonPropertyName("feedbackInformation")]
    [Description("Feedback information of Control")]
    public FeedbackInformation? FeedbackInformation { get; init; }

    [JsonPropertyName("detailsOnReExport")]
    [Description("Details on re-export")]
    public DetailsOnReExport? DetailsOnReExport { get; init; }

    [JsonPropertyName("officialInspector")]
    [Description("Official inspector")]
    public OfficialInspector? OfficialInspector { get; init; }

    [JsonPropertyName("consignmentLeave")]
    [ExampleValue("YES")]
    [ExampleValue("NO")]
    [ExampleValue("It has been destroyed")]
    [Description("Is the consignment leaving UK borders?. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ConsignmentLeave { get; init; }
}
