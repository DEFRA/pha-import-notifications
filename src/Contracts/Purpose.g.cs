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
    [ExampleValue("AnimalFeedingStuff")]
    [ExampleValue("HumanConsumption")]
    [ExampleValue("PharmaceuticalUse")]
    [ExampleValue("TechnicalUse")]
    [ExampleValue("Other")]
    [ExampleValue("CommercialSale")]
    [ExampleValue("CommercialSaleOrChangeOfOwnership")]
    [ExampleValue("Rescue")]
    [ExampleValue("Breeding")]
    [ExampleValue("Research")]
    [ExampleValue("RacingOrCompetition")]
    [ExampleValue("ApprovedPremisesOrBody")]
    [ExampleValue("CompanionAnimalNotForResaleOrRehoming")]
    [ExampleValue("Production")]
    [ExampleValue("Slaughter")]
    [ExampleValue("Fattening")]
    [ExampleValue("GameRestocking")]
    [ExampleValue("RegisteredHorses")]
    [Description("Detailed purpose of internal market purpose group")]
    public string? InternalMarketPurpose { get; init; }

    [JsonPropertyName("thirdCountryTranshipment")]
    [Description("Country that consignment is transshipped through")]
    public string? ThirdCountryTranshipment { get; init; }

    [JsonPropertyName("forNonConforming")]
    [ExampleValue("CustomsWarehouse")]
    [ExampleValue("FreeZoneOrFreeWarehouse")]
    [ExampleValue("ShipSupplier")]
    [ExampleValue("Ship")]
    [Description("Detailed purpose for non conforming purpose group")]
    public string? ForNonConforming { get; init; }

    [JsonPropertyName("regNumber")]
    [Description("There are 3 types of registration number based on the purpose of consignment. Customs registration number, Free zone registration number and Shipping supplier registration number. ")]
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
    [ExampleValue("DefinitiveImport")]
    [ExampleValue("HorsesReEntry")]
    [ExampleValue("TemporaryAdmissionHorses")]
    [Description("Specification of Import or admission purpose")]
    public string? ForImportOrAdmission { get; init; }

    [JsonPropertyName("exitDate")]
    [Description("Exit date when import or admission")]
    public DateOnly? ExitDate { get; init; }

    [JsonPropertyName("finalBip")]
    [Description("Final Border Inspection Post")]
    public string? FinalBip { get; init; }

    [JsonPropertyName("purposeGroup")]
    [ExampleValue("ForImport")]
    [ExampleValue("ForNONConformingConsignments")]
    [ExampleValue("ForTranshipmentTo")]
    [ExampleValue("ForTransitTo3rdCountry")]
    [ExampleValue("ForReImport")]
    [ExampleValue("ForPrivateImport")]
    [ExampleValue("ForTransferTo")]
    [ExampleValue("ForImportReConformityCheck")]
    [ExampleValue("ForImportNonInternalMarket")]
    [Description("Purpose group of consignment (general purpose)")]
    public string? PurposeGroup { get; init; }

    [JsonPropertyName("estimatedArrivesAtPortOfExit")]
    [Description("DateTime")]
    public DateTime? EstimatedArrivesAtPortOfExit { get; init; }
}
