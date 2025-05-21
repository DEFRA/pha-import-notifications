#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Purpose
{
    [JsonPropertyName("conformsToEU")]
    [Description("Does consignment conforms to UK laws")]
    public bool? ConformsToEU { get; init; }

    [JsonPropertyName("internalMarketPurpose")]
    [ExampleValue("Animal Feeding Stuff")]
    [ExampleValue("Human Consumption")]
    [ExampleValue("Pharmaceutical Use")]
    [ExampleValue("Technical Use")]
    [ExampleValue("Other")]
    [ExampleValue("Commercial Sale")]
    [ExampleValue("Commercial sale or change of ownership")]
    [ExampleValue("Rescue")]
    [ExampleValue("Breeding")]
    [ExampleValue("Research")]
    [ExampleValue("Racing or Competition")]
    [ExampleValue("Approved Premises or Body")]
    [ExampleValue("Companion Animal not for Resale or Rehoming")]
    [ExampleValue("Production")]
    [ExampleValue("Slaughter")]
    [ExampleValue("Fattening")]
    [ExampleValue("Game Restocking")]
    [ExampleValue("Registered Horses")]
    [Description("Detailed purpose of internal market purpose group. Possible values taken from IPAFFS schema version 17.5.")]
    public string? InternalMarketPurpose { get; init; }

    [JsonPropertyName("thirdCountryTranshipment")]
    [Description("Country that consignment is transshipped through")]
    public string? ThirdCountryTranshipment { get; init; }

    [JsonPropertyName("forNonConforming")]
    [ExampleValue("Customs Warehouse")]
    [ExampleValue("Free Zone or Free Warehouse")]
    [ExampleValue("Ship Supplier")]
    [ExampleValue("Ship")]
    [Description("Detailed purpose for non conforming purpose group. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ForNonConforming { get; init; }

    [JsonPropertyName("regNumber")]
    [Description("There are 3 types of registration number based on the purpose of consignment. Customs registration number, Free zone registration number and Shipping supplier registration number.")]
    public string? RegNumber { get; init; }

    [JsonPropertyName("shipName")]
    [Description("Ship name")]
    public string? ShipName { get; init; }

    [JsonPropertyName("shipPort")]
    [Description("Destination Ship port")]
    public string? ShipPort { get; init; }

    [JsonPropertyName("exitBip")]
    [Description("Exit Border Inspection Post")]
    public string? ExitBip { get; init; }

    [JsonPropertyName("thirdCountry")]
    [Description("Country to which consignment is transited")]
    public string? ThirdCountry { get; init; }

    [JsonPropertyName("transitThirdCountries")]
    [Description("Countries that consignment is transited through")]
    public List<string>? TransitThirdCountries { get; init; }

    [JsonPropertyName("forImportOrAdmission")]
    [ExampleValue("Definitive import")]
    [ExampleValue("Horses Re-entry")]
    [ExampleValue("Temporary admission horses")]
    [Description("Specification of Import or admission purpose. Possible values taken from IPAFFS schema version 17.5.")]
    public string? ForImportOrAdmission { get; init; }

    [JsonPropertyName("exitDate")]
    [Description("Exit date when import or admission")]
    public DateOnly? ExitDate { get; init; }

    [JsonPropertyName("finalBip")]
    [Description("Final Border Inspection Post")]
    public string? FinalBip { get; init; }

    [JsonPropertyName("pointOfExit")]
    [Description("Place of departure")]
    public string? PointOfExit { get; init; }

    [JsonPropertyName("purposeGroup")]
    [ExampleValue("For Import")]
    [ExampleValue("For NON-Conforming Consignments")]
    [ExampleValue("For Transhipment to")]
    [ExampleValue("For Transit to 3rd Country")]
    [ExampleValue("For Re-Import")]
    [ExampleValue("For Private Import")]
    [ExampleValue("For Transfer To")]
    [ExampleValue("For Import Re-Conformity Check")]
    [Description("Purpose group of consignment (general purpose). Possible values taken from IPAFFS schema version 17.5.")]
    public string? PurposeGroup { get; init; }

    [JsonPropertyName("estimatedArrivesAtPortOfExit")]
    [Description("DateTime")]
    public DateTime? EstimatedArrivesAtPortOfExit { get; init; }
}
