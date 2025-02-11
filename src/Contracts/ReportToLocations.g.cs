#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ReportToLocations
{
    [JsonPropertyName("inspectionTypeId")]
    [Description("An inspectionTypeId from GVMS Reference Data denoting the type of inspection that needs to be performed on the vehicle.")]
    public string? InspectionTypeId { get; init; }

    [JsonPropertyName("locationIds")]
    [Description("A list of locationIds from GVMS Reference Data that are available to perform this type of inspection.")]
    public List<string>? LocationIds { get; init; }
}
