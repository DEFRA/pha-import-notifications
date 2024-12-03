#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Control
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
    [Description("Is the consignment leaving UK borders?")]
    public ControlConsignmentLeaveEnum? ConsignmentLeave { get; init; }
}
