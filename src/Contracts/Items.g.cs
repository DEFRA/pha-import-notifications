#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Items
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

    [JsonPropertyName("itemNetMass")]
    public decimal? ItemNetMass { get; init; }

    [JsonPropertyName("itemSupplementaryUnits")]
    public decimal? ItemSupplementaryUnits { get; init; }

    [JsonPropertyName("itemThirdQuantity")]
    public decimal? ItemThirdQuantity { get; init; }

    [JsonPropertyName("itemOriginCountryCode")]
    public string? ItemOriginCountryCode { get; init; }

    [JsonPropertyName("documents")]
    public List<Document>? Documents { get; init; }

    [JsonPropertyName("checks")]
    public List<Check>? Checks { get; init; }
}
