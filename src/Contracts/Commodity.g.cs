#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Commodity
{
    [JsonPropertyName("itemNumber")]
    public int? ItemNumber { get; init; }

    [JsonPropertyName("customsProcedureCode")]
    public string? CustomsProcedureCode { get; init; }

    [JsonPropertyName("taricCommodityCode")]
    public string? TaricCommodityCode { get; init; }

    [JsonPropertyName("goodsDescription")]
    public string? GoodsDescription { get; init; }

    [JsonPropertyName("consigneeId")]
    public string? ConsigneeId { get; init; }

    [JsonPropertyName("consigneeName")]
    public string? ConsigneeName { get; init; }

    [JsonPropertyName("netMass")]
    public decimal? NetMass { get; init; }

    [JsonPropertyName("supplementaryUnits")]
    public decimal? SupplementaryUnits { get; init; }

    [JsonPropertyName("thirdQuantity")]
    public decimal? ThirdQuantity { get; init; }

    [JsonPropertyName("originCountryCode")]
    public string? OriginCountryCode { get; init; }

    [JsonPropertyName("documents")]
    public List<ImportDocument>? Documents { get; init; }

    [JsonPropertyName("checks")]
    public List<CustomsCommodityCheck>? Checks { get; init; }
}
