#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ReportToLocations
{
    [JsonPropertyName("inspectionTypeId")]
    public string? InspectionTypeId { get; init; }
}
