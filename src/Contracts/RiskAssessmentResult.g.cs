#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record RiskAssessmentResult
{
    [JsonPropertyName("commodityResults")]
    [Description("List of risk assessed commodities")]
    public List<CommodityRiskResult>? CommodityResults { get; init; }

    [JsonPropertyName("assessedOn")]
    [Description("Date and time of assessment")]
    public DateTime? AssessedOn { get; init; }
}
