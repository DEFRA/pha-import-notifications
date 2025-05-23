#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record NotificationCommodityCheck
{
    [JsonPropertyName("uniqueComplementId")]
    [Description("UUID used to match the commodityChecks to the commodityComplement")]
    public string? UniqueComplementId { get; init; }

    [JsonPropertyName("checks")]
    public List<InspectionCheck>? Checks { get; init; }

    [JsonPropertyName("validityPeriod")]
    [Description("Manually entered validity period, allowed if risk decision is INSPECTION_REQUIRED and HMI check status 'Compliant' or 'Not inspected'")]
    public required int ValidityPeriod { get; init; }
}
