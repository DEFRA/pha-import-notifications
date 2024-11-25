#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Purpose
{
    [JsonPropertyName("conformsToEU")]
    [Description("Does consignment conforms to UK laws")]
    public bool? ConformsToEU { get; set; }

    [JsonPropertyName("internalMarketPurpose")]
    public PurposeInternalMarketPurposeEnum InternalMarketPurpose { get; set; }

    [JsonPropertyName("thirdCountryTranshipment")]
    [Description("Country that consignment is transshipped through")]
    public string? ThirdCountryTranshipment { get; set; }

    [JsonPropertyName("forNonConforming")]
    public PurposeForNonConformingEnum ForNonConforming { get; set; }

    [JsonPropertyName("regNumber")]
    [Description("There are 3 types of registration number based on the purpose of consignment. Customs registration number, Free zone registration number and Shipping supplier registration number.")]
    public string? RegNumber { get; set; }

    [JsonPropertyName("shipName")]
    [Description("Ship name")]
    public string? ShipName { get; set; }

    [JsonPropertyName("shipPort")]
    [Description("Destination Ship port")]
    public string? ShipPort { get; set; }

    [JsonPropertyName("exitBip")]
    [Description("Exit Border Inspection Post")]
    public string? ExitBip { get; set; }

    [JsonPropertyName("thirdCountry")]
    [Description("Country to which consignment is transited")]
    public string? ThirdCountry { get; set; }

    [JsonPropertyName("transitThirdCountries")]
    [Description("Countries that consignment is transited through")]
    public List<string>? TransitThirdCountries { get; set; }

    [JsonPropertyName("forImportOrAdmission")]
    public PurposeForImportOrAdmissionEnum ForImportOrAdmission { get; set; }

    [JsonPropertyName("exitDate")]
    [Description("Exit date when import or admission")]
    public string? ExitDate { get; set; }

    [JsonPropertyName("finalBip")]
    [Description("Final Border Inspection Post")]
    public string? FinalBip { get; set; }

    [JsonPropertyName("purposeGroup")]
    public PurposePurposeGroupEnum PurposeGroup { get; set; }

    [JsonPropertyName("estimatedArrivedAtPortOfExit")]
    [Description("DateTime")]
    public DateTime? EstimatedArrivedAtPortOfExit { get; set; }
}
