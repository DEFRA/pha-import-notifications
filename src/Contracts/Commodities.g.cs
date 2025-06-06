#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Commodities
{
    [JsonPropertyName("gmsDeclarationAccepted")]
    [Description("Flag to record when the GMS declaration has been accepted")]
    public bool? GmsDeclarationAccepted { get; init; }

    [JsonPropertyName("consignedCountryInChargeGroup")]
    [Description("Flag to record whether the consigned country is in an ipaffs charge group")]
    public bool? ConsignedCountryInChargeGroup { get; init; }

    [JsonPropertyName("totalGrossWeight")]
    [Description("The total gross weight of the consignment.  It must be bigger than the total net weight of the commodities")]
    public decimal? TotalGrossWeight { get; init; }

    [JsonPropertyName("totalNetWeight")]
    [Description("The total net weight of the commodities within this consignment")]
    public decimal? TotalNetWeight { get; init; }

    [JsonPropertyName("totalGrossVolume")]
    [Description("The total gross volume of the commodities within this consignment")]
    public decimal? TotalGrossVolume { get; init; }

    [JsonPropertyName("totalGrossVolumeUnit")]
    [Description("Unit used for specifying total gross volume of this consignment (litres or metres cubed)")]
    public string? TotalGrossVolumeUnit { get; init; }

    [JsonPropertyName("numberOfPackages")]
    [Description("The total number of packages within this consignment")]
    public int? NumberOfPackages { get; init; }

    [JsonPropertyName("temperature")]
    [Description("Temperature (type) of commodity")]
    public string? Temperature { get; init; }

    [JsonPropertyName("numberOfAnimals")]
    [Description("The total number of animals within this consignment")]
    public int? NumberOfAnimals { get; init; }

    [JsonPropertyName("includeNonAblactedAnimals")]
    [Description("Does consignment contain ablacted animals")]
    public bool? IncludeNonAblactedAnimals { get; init; }

    [JsonPropertyName("countryOfOrigin")]
    [Description("Consignments country of origin")]
    public string? CountryOfOrigin { get; init; }

    [JsonPropertyName("countryOfOriginIsPodCountry")]
    [Description("Flag to record whether country of origin is a temporary PoD country")]
    public bool? CountryOfOriginIsPodCountry { get; init; }

    [JsonPropertyName("isLowRiskArticle72Country")]
    [Description("Flag to record whether country of origin is a low risk article 72 country")]
    public bool? IsLowRiskArticle72Country { get; init; }

    [JsonPropertyName("regionOfOrigin")]
    [Description("Region of country")]
    public string? RegionOfOrigin { get; init; }

    [JsonPropertyName("consignedCountry")]
    [Description("Country from where commodity was sent")]
    public string? ConsignedCountry { get; init; }

    [JsonPropertyName("animalsCertifiedAs")]
    [Description("Certification of animals (Breeding, slaughter etc.)")]
    public string? AnimalsCertifiedAs { get; init; }

    [JsonPropertyName("commodityIntendedFor")]
    [ExampleValue("human")]
    [ExampleValue("feedingstuff")]
    [ExampleValue("further")]
    [ExampleValue("other")]
    [Description("What the commodity is intended for. Possible values taken from IPAFFS schema version 17.5.")]
    public string? CommodityIntendedFor { get; init; }

    [JsonPropertyName("commodityComplements")]
    [Description("Holder for additional parameters of a commodity")]
    public List<CommodityComplements>? CommodityComplements { get; init; }

    [JsonPropertyName("complementParameterSets")]
    [Description("Additional data for commodityComplement part containing such data as net weight")]
    public List<ComplementParameterSets>? ComplementParameterSets { get; init; }
}
