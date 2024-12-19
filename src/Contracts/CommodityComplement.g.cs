#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class CommodityComplement
{
    [JsonPropertyName("uniqueComplementId")]
    [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
    public string? UniqueComplementId { get; init; }

    [JsonPropertyName("commodityDescription")]
    [Description("Description of the commodity")]
    public string? CommodityDescription { get; init; }

    [JsonPropertyName("commodityId")]
    [Description("The unique commodity ID")]
    public string? CommodityId { get; init; }

    [JsonPropertyName("complementId")]
    [Description("Identifier of complement chosen from speciesFamily,speciesClass and speciesType'. This is also used to link to the complementParameterSet")]
    public int? ComplementId { get; init; }

    [JsonPropertyName("complementName")]
    [Description("Represents the lowest granularity - either type, class, family or species name - for the commodity selected.  This is also used to drive behaviour for EU Import journeys")]
    public string? ComplementName { get; init; }

    [JsonPropertyName("eppoCode")]
    [Description("EPPO Code related to plant commodities and wood packaging")]
    public string? EppoCode { get; init; }

    [JsonPropertyName("isWoodPackaging")]
    [Description("(Deprecated in IMTA-11832) Is this commodity wood packaging?")]
    public bool? IsWoodPackaging { get; init; }

    [JsonPropertyName("speciesId")]
    [Description("The species ID of the commodity that is imported. Not every commodity has a species ID. This is also used to link to the complementParameterSet. The species ID can change over time")]
    public string? SpeciesId { get; init; }

    [JsonPropertyName("speciesName")]
    [Description("Species name")]
    public string? SpeciesName { get; init; }

    [JsonPropertyName("speciesNomination")]
    [Description("Species nomination")]
    public string? SpeciesNomination { get; init; }

    [JsonPropertyName("speciesTypeName")]
    [Description("Species type name")]
    public string? SpeciesTypeName { get; init; }

    [JsonPropertyName("speciesType")]
    [Description("Species type identifier of commodity")]
    public string? SpeciesType { get; init; }

    [JsonPropertyName("speciesClassName")]
    [Description("Species class name")]
    public string? SpeciesClassName { get; init; }

    [JsonPropertyName("speciesClass")]
    [Description("Species class identifier of commodity")]
    public string? SpeciesClass { get; init; }

    [JsonPropertyName("speciesFamilyName")]
    [Description("Species family name of commodity")]
    public string? SpeciesFamilyName { get; init; }

    [JsonPropertyName("speciesFamily")]
    [Description("Species family identifier of commodity")]
    public string? SpeciesFamily { get; init; }

    [JsonPropertyName("speciesCommonName")]
    [Description("Species common name of commodity for IMP notification simple commodity selection")]
    public string? SpeciesCommonName { get; init; }

    [JsonPropertyName("isCdsMatched")]
    [Description("Has commodity been matched with corresponding CDS declaration")]
    public bool? IsCdsMatched { get; init; }

    [JsonPropertyName("additionalData")]
    public object? AdditionalData { get; init; }

    [JsonPropertyName("riskAssesment")]
    public CommodityRiskResult? RiskAssesment { get; init; }

    [JsonPropertyName("checks")]
    public List<InspectionCheck>? Checks { get; init; }
}
