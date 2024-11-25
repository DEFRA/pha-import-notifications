#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Control
{
    [JsonPropertyName("feedbackInformation")]
    public FeedbackInformation FeedbackInformation { get; set; }

    [JsonPropertyName("detailsOnReExport")]
    public DetailsOnReExport DetailsOnReExport { get; set; }

    [JsonPropertyName("officialInspector")]
    public OfficialInspector OfficialInspector { get; set; }

    [JsonPropertyName("consignmentLeave")]
    public ControlConsignmentLeaveEnum ConsignmentLeave { get; set; }
}
