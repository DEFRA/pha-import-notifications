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
    [ExampleValue("Yes")]
    [ExampleValue("No")]
    [ExampleValue("ItHasBeenDestroyed")]
    [Description("Is the consignment leaving UK borders?")]
    public string? ConsignmentLeave { get; init; }
}
