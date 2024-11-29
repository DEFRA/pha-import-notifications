#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Gmr
{
    [JsonPropertyName("auditEntries")]
    public List<AuditEntry>? AuditEntries { get; init; }

    [JsonPropertyName("gmrId")]
    public string? GmrId { get; init; }

    [JsonPropertyName("haulierEori")]
    public string? HaulierEori { get; init; }

    [JsonPropertyName("state")]
    public required StateEnum State { get; init; }

    [JsonPropertyName("inspectionRequired")]
    public bool? InspectionRequired { get; init; }

    [JsonPropertyName("reportToLocations")]
    public List<ReportToLocations>? ReportToLocations { get; init; }

    [JsonPropertyName("lastUpdated")]
    public DateTime? LastUpdated { get; init; }

    [JsonPropertyName("direction")]
    public required DirectionEnum Direction { get; init; }

    [JsonPropertyName("haulierType")]
    public required HaulierTypeEnum HaulierType { get; init; }

    [JsonPropertyName("isUnaccompanied")]
    public bool? IsUnaccompanied { get; init; }

    [JsonPropertyName("vehicleRegistrationNumber")]
    public string? VehicleRegistrationNumber { get; init; }

    [JsonPropertyName("plannedCrossing")]
    public required PlannedCrossing PlannedCrossing { get; init; }

    [JsonPropertyName("checkedInCrossing")]
    public required CheckedInCrossing CheckedInCrossing { get; init; }

    [JsonPropertyName("actualCrossing")]
    public required ActualCrossing ActualCrossing { get; init; }
}
