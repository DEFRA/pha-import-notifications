using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Control
{
    [JsonPropertyName("feedbackInformation")]
    public required FeedbackInformation FeedbackInformation { get; init; }

    [JsonPropertyName("detailsOnReExport")]
    public required DetailsOnReExport DetailsOnReExport { get; init; }

    [JsonPropertyName("officialInspector")]
    public required OfficialInspector OfficialInspector { get; init; }

    [JsonPropertyName("consignmentLeave")]
    public required ControlConsignmentLeaveEnum ConsignmentLeave { get; init; }
}
