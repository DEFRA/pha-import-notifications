#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Decision
{
    [JsonPropertyName("consignmentAcceptable")]
    [Description("Is consignment acceptable or not")]
    public bool? ConsignmentAcceptable { get; init; }

    [JsonPropertyName("notAcceptableAction")]
    [ExampleValue("Slaughter")]
    [ExampleValue("Reexport")]
    [ExampleValue("Euthanasia")]
    [ExampleValue("Redispatching")]
    [ExampleValue("Destruction")]
    [ExampleValue("Transformation")]
    [ExampleValue("Other")]
    [ExampleValue("EntryRefusal")]
    [ExampleValue("QuarantineImposed")]
    [ExampleValue("SpecialTreatment")]
    [ExampleValue("IndustrialProcessing")]
    [ExampleValue("ReDispatch")]
    [ExampleValue("UseForOtherPurposes")]
    [Description("Filled if consignmentAcceptable is set to false")]
    public string? NotAcceptableAction { get; init; }

    [JsonPropertyName("notAcceptableActionDestructionReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to destruction")]
    public string? NotAcceptableActionDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionEntryRefusalReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to entry refusal")]
    public string? NotAcceptableActionEntryRefusalReason { get; init; }

    [JsonPropertyName("notAcceptableActionQuarantineImposedReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to quarantine imposed")]
    public string? NotAcceptableActionQuarantineImposedReason { get; init; }

    [JsonPropertyName("notAcceptableActionSpecialTreatmentReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to special treatment")]
    public string? NotAcceptableActionSpecialTreatmentReason { get; init; }

    [JsonPropertyName("notAcceptableActionIndustrialProcessingReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to industrial processing")]
    public string? NotAcceptableActionIndustrialProcessingReason { get; init; }

    [JsonPropertyName("notAcceptableActionReDispatchReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to re-dispatch")]
    public string? NotAcceptableActionReDispatchReason { get; init; }

    [JsonPropertyName("notAcceptableActionUseForOtherPurposesReason")]
    [ExampleValue("ContaminatedProducts")]
    [ExampleValue("InterceptedPart")]
    [ExampleValue("PackagingMaterial")]
    [ExampleValue("MeansOfTransport")]
    [ExampleValue("Other")]
    [Description("Filled if not acceptable action is set to use for other purposes")]
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
    [Description("Filled if consignment is set to acceptable and decision type is Specific Warehouse")]
    public string? SpecificWarehouseNonConformingConsignment { get; init; }

    [JsonPropertyName("temporaryDeadline")]
    [Description("Deadline when consignment has to leave borders")]
    public string? TemporaryDeadline { get; init; }

    [JsonPropertyName("decisionEnum")]
    [ExampleValue("NonAcceptable")]
    [ExampleValue("AcceptableForInternalMarket")]
    [ExampleValue("AcceptableIfChanneled")]
    [ExampleValue("AcceptableForTranshipment")]
    [ExampleValue("AcceptableForTransit")]
    [ExampleValue("AcceptableForTemporaryImport")]
    [ExampleValue("AcceptableForSpecificWarehouse")]
    [ExampleValue("AcceptableForPrivateImport")]
    [ExampleValue("AcceptableForTransfer")]
    [ExampleValue("HorseReEntry")]
    [Description("Detailed decision for consignment")]
    public string? DecisionEnum { get; init; }

    [JsonPropertyName("freeCirculationPurpose")]
    [ExampleValue("AnimalFeedingStuff")]
    [ExampleValue("HumanConsumption")]
    [ExampleValue("PharmaceuticalUse")]
    [ExampleValue("TechnicalUse")]
    [ExampleValue("FurtherProcess")]
    [ExampleValue("Other")]
    [Description("Decision over purpose of free circulation in country")]
    public string? FreeCirculationPurpose { get; init; }

    [JsonPropertyName("definitiveImportPurpose")]
    [ExampleValue("Slaughter")]
    [ExampleValue("Approvedbodies")]
    [ExampleValue("Quarantine")]
    [Description("Decision over purpose of definitive import")]
    public string? DefinitiveImportPurpose { get; init; }

    [JsonPropertyName("ifChanneledOption")]
    [ExampleValue("Article8")]
    [ExampleValue("Article15")]
    [Description("Decision channeled option based on (article8, article15)")]
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
