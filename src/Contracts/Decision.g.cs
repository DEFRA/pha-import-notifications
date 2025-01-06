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
    [Description("Filled if consignmentAcceptable is set to false")]
    public DecisionNotAcceptableActionEnum? NotAcceptableAction { get; init; }

    [JsonPropertyName("notAcceptableActionDestructionReason")]
    [Description("Filled if not acceptable action is set to destruction")]
    public DecisionNotAcceptableActionDestructionReasonEnum? NotAcceptableActionDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionEntryRefusalReason")]
    [Description("Filled if not acceptable action is set to entry refusal")]
    public DecisionNotAcceptableActionEntryRefusalReasonEnum? NotAcceptableActionEntryRefusalReason { get; init; }

    [JsonPropertyName("notAcceptableActionQuarantineImposedReason")]
    [Description("Filled if not acceptable action is set to quarantine imposed")]
    public DecisionNotAcceptableActionQuarantineImposedReasonEnum? NotAcceptableActionQuarantineImposedReason { get; init; }

    [JsonPropertyName("notAcceptableActionSpecialTreatmentReason")]
    [Description("Filled if not acceptable action is set to special treatment")]
    public DecisionNotAcceptableActionSpecialTreatmentReasonEnum? NotAcceptableActionSpecialTreatmentReason { get; init; }

    [JsonPropertyName("notAcceptableActionIndustrialProcessingReason")]
    [Description("Filled if not acceptable action is set to industrial processing")]
    public DecisionNotAcceptableActionIndustrialProcessingReasonEnum? NotAcceptableActionIndustrialProcessingReason { get; init; }

    [JsonPropertyName("notAcceptableActionReDispatchReason")]
    [Description("Filled if not acceptable action is set to re-dispatch")]
    public DecisionNotAcceptableActionReDispatchReasonEnum? NotAcceptableActionReDispatchReason { get; init; }

    [JsonPropertyName("notAcceptableActionUseForOtherPurposesReason")]
    [Description("Filled if not acceptable action is set to use for other purposes")]
    public DecisionNotAcceptableActionUseForOtherPurposesReasonEnum? NotAcceptableActionUseForOtherPurposesReason { get; init; }

    [JsonPropertyName("notAcceptableDestructionReason")]
    [Description("Filled when notAcceptableAction is equal to destruction")]
    public string? NotAcceptableDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionOtherReason")]
    [Description("Filled when notAcceptableAction is equal to other")]
    public string? NotAcceptableActionOtherReason { get; init; }

    [JsonPropertyName("notAcceptableActionByDate")]
    [Description("Filled when consignmentAcceptable is set to false")]
    public string? NotAcceptableActionByDate { get; init; }

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
    [Description("Filled if consignment is set to acceptable and decision type is Specific Warehouse")]
    public DecisionSpecificWarehouseNonConformingConsignmentEnum? SpecificWarehouseNonConformingConsignment { get; init; }

    [JsonPropertyName("temporaryDeadline")]
    [Description("Deadline when consignment has to leave borders")]
    public string? TemporaryDeadline { get; init; }

    [JsonPropertyName("decisionEnum")]
    [Description("Detailed decision for consignment")]
    public DecisionDecisionEnum? DecisionEnum { get; init; }

    [JsonPropertyName("freeCirculationPurpose")]
    [Description("Decision over purpose of free circulation in country")]
    public DecisionFreeCirculationPurposeEnum? FreeCirculationPurpose { get; init; }

    [JsonPropertyName("definitiveImportPurpose")]
    [Description("Decision over purpose of definitive import")]
    public DecisionDefinitiveImportPurposeEnum? DefinitiveImportPurpose { get; init; }

    [JsonPropertyName("ifChanneledOption")]
    [Description("Decision channeled option based on (article8, article15)")]
    public DecisionIfChanneledOptionEnum? IfChanneledOption { get; init; }

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
