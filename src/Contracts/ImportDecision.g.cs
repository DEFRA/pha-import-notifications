#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportDecision
{
    [JsonPropertyName("consignmentAcceptable")]
    [Description("Is consignment acceptable or not")]
    public bool? ConsignmentAcceptable { get; init; }

    [JsonPropertyName("notAcceptableAction")]
    [ExampleValue("slaughter")]
    [ExampleValue("reexport")]
    [ExampleValue("euthanasia")]
    [ExampleValue("redispatching")]
    [ExampleValue("destruction")]
    [ExampleValue("transformation")]
    [ExampleValue("other")]
    [ExampleValue("entry-refusal")]
    [ExampleValue("quarantine-imposed")]
    [ExampleValue("special-treatment")]
    [ExampleValue("industrial-processing")]
    [ExampleValue("re-dispatch")]
    [ExampleValue("use-for-other-purposes")]
    [Description("Filled if consignmentAcceptable is set to false. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableAction { get; init; }

    [JsonPropertyName("notAcceptableActionDestructionReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to destruction. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionEntryRefusalReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to entry refusal. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionEntryRefusalReason { get; init; }

    [JsonPropertyName("notAcceptableActionQuarantineImposedReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to quarantine imposed. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionQuarantineImposedReason { get; init; }

    [JsonPropertyName("notAcceptableActionSpecialTreatmentReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to special treatment. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionSpecialTreatmentReason { get; init; }

    [JsonPropertyName("notAcceptableActionIndustrialProcessingReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to industrial processing. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionIndustrialProcessingReason { get; init; }

    [JsonPropertyName("notAcceptableActionReDispatchReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to re-dispatch. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionReDispatchReason { get; init; }

    [JsonPropertyName("notAcceptableActionUseForOtherPurposesReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to use for other purposes. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NotAcceptableActionUseForOtherPurposesReason { get; init; }

    [JsonPropertyName("notAcceptableDestructionReason")]
    [Description("Filled when notAcceptableAction is equal to destruction")]
    public string? NotAcceptableDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionOtherReason")]
    [Description("Filled when notAcceptableAction is equal to other")]
    public string? NotAcceptableActionOtherReason { get; init; }

    [JsonPropertyName("notAcceptableActionByDate")]
    [Description("Filled when consignmentAcceptable is set to false")]
    public DateOnly? NotAcceptableActionByDate { get; init; }

    [JsonPropertyName("chedppNotAcceptableReasons")]
    [Description("List of details for individual chedpp not acceptable reasons")]
    public List<ChedppNotAcceptableReason>? ChedppNotAcceptableReasons { get; init; }

    [JsonPropertyName("notAcceptableReasons")]
    [Description("If the consignment was not accepted what was the reason")]
    public List<string>? NotAcceptableReasons { get; init; }

    [JsonPropertyName("notAcceptableCountry")]
    [Description("2 digits ISO code of country (not acceptable country can be empty)")]
    public string? NotAcceptableCountry { get; init; }

    [JsonPropertyName("notAcceptableEstablishment")]
    [Description("Filled if consignmentAcceptable is set to false")]
    public string? NotAcceptableEstablishment { get; init; }

    [JsonPropertyName("notAcceptableOtherReason")]
    [Description("Filled if consignmentAcceptable is set to false")]
    public string? NotAcceptableOtherReason { get; init; }

    [JsonPropertyName("detailsOfControlledDestinations")]
    [Description("Details of controlled destinations")]
    public Party? DetailsOfControlledDestinations { get; init; }

    [JsonPropertyName("specificWarehouseNonConformingConsignment")]
    [ExampleValue("CustomWarehouse")]
    [ExampleValue("FreeZoneOrFreeWarehouse")]
    [ExampleValue("ShipSupplier")]
    [ExampleValue("Ship")]
    [Description("Filled if consignment is set to acceptable and decision type is Specific Warehouse. Possible values taken from IPAFFS schema version 17.5.")]
    public string? SpecificWarehouseNonConformingConsignment { get; init; }

    [JsonPropertyName("temporaryDeadline")]
    [Description("Deadline when consignment has to leave borders")]
    public string? TemporaryDeadline { get; init; }

    [JsonPropertyName("decision")]
    [ExampleValue("Non Acceptable")]
    [ExampleValue("Acceptable for Internal Market")]
    [ExampleValue("Acceptable if Channeled")]
    [ExampleValue("Acceptable for Transhipment")]
    [ExampleValue("Acceptable for Transit")]
    [ExampleValue("Acceptable for Temporary Import")]
    [ExampleValue("Acceptable for Specific Warehouse")]
    [ExampleValue("Acceptable for Private Import")]
    [ExampleValue("Acceptable for Transfer")]
    [ExampleValue("Horse Re-entry")]
    [Description("Detailed decision for consignment. Possible values taken from IPAFFS schema version 17.5.")]
    public string? Decision { get; init; }

    [JsonPropertyName("freeCirculationPurpose")]
    [ExampleValue("Animal Feeding Stuff")]
    [ExampleValue("Human Consumption")]
    [ExampleValue("Pharmaceutical Use")]
    [ExampleValue("Technical Use")]
    [ExampleValue("Further Process")]
    [ExampleValue("Other")]
    [Description("Decision over purpose of free circulation in country. Possible values taken from IPAFFS schema version 17.5.")]
    public string? FreeCirculationPurpose { get; init; }

    [JsonPropertyName("definitiveImportPurpose")]
    [ExampleValue("slaughter")]
    [ExampleValue("approvedbodies")]
    [ExampleValue("quarantine")]
    [Description("Decision over purpose of definitive import. Possible values taken from IPAFFS schema version 17.5.")]
    public string? DefinitiveImportPurpose { get; init; }

    [JsonPropertyName("ifChanneledOption")]
    [ExampleValue("article8")]
    [ExampleValue("article15")]
    [Description("Decision channeled option based on (article8, article15). Possible values taken from IPAFFS schema version 17.5.")]
    public string? IfChanneledOption { get; init; }

    [JsonPropertyName("customWarehouseRegisteredNumber")]
    [Description("Custom warehouse registered number")]
    public string? CustomWarehouseRegisteredNumber { get; init; }

    [JsonPropertyName("freeWarehouseRegisteredNumber")]
    [Description("Free warehouse registered number")]
    public string? FreeWarehouseRegisteredNumber { get; init; }

    [JsonPropertyName("shipName")]
    [Description("Ship name")]
    public string? ShipName { get; init; }

    [JsonPropertyName("shipPortOfExit")]
    [Description("Port of exit")]
    public string? ShipPortOfExit { get; init; }

    [JsonPropertyName("shipSupplierRegisteredNumber")]
    [Description("Ship supplier registered number")]
    public string? ShipSupplierRegisteredNumber { get; init; }

    [JsonPropertyName("transhipmentBip")]
    [Description("Transhipment BIP")]
    public string? TranshipmentBip { get; init; }

    [JsonPropertyName("transhipmentThirdCountry")]
    [Description("Transhipment third country")]
    public string? TranshipmentThirdCountry { get; init; }

    [JsonPropertyName("transitExitBip")]
    [Description("Transit exit BIP")]
    public string? TransitExitBip { get; init; }

    [JsonPropertyName("transitThirdCountry")]
    [Description("Transit third country")]
    public string? TransitThirdCountry { get; init; }

    [JsonPropertyName("transitDestinationThirdCountry")]
    [Description("Transit destination third country")]
    public string? TransitDestinationThirdCountry { get; init; }

    [JsonPropertyName("temporaryExitBip")]
    [Description("Temporary exit BIP")]
    public string? TemporaryExitBip { get; init; }

    [JsonPropertyName("horseReentry")]
    [Description("Horse re-entry")]
    public string? HorseReentry { get; init; }

    [JsonPropertyName("transhipmentEuOrThirdCountry")]
    [Description("Is it transshipped to EU or third country (values EU / country name)")]
    public string? TranshipmentEuOrThirdCountry { get; init; }
}
