#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Decision
{
    [JsonPropertyName("consignmentAcceptable")]
    [Description("Is consignment acceptable or not")]
    public bool? ConsignmentAcceptable { get; init; }

    [JsonPropertyName("notAcceptableAction")]
    public required DecisionNotAcceptableActionEnum NotAcceptableAction { get; init; }

    [JsonPropertyName("notAcceptableActionDestructionReason")]
    public required DecisionNotAcceptableActionDestructionReasonEnum NotAcceptableActionDestructionReason { get; init; }

    [JsonPropertyName("notAcceptableActionEntryRefusalReason")]
    public required DecisionNotAcceptableActionEntryRefusalReasonEnum NotAcceptableActionEntryRefusalReason { get; init; }

    [JsonPropertyName("notAcceptableActionQuarantineImposedReason")]
    public required DecisionNotAcceptableActionQuarantineImposedReasonEnum NotAcceptableActionQuarantineImposedReason { get; init; }

    [JsonPropertyName("notAcceptableActionSpecialTreatmentReason")]
    public required DecisionNotAcceptableActionSpecialTreatmentReasonEnum NotAcceptableActionSpecialTreatmentReason { get; init; }

    [JsonPropertyName("notAcceptableActionIndustrialProcessingReason")]
    public required DecisionNotAcceptableActionIndustrialProcessingReasonEnum NotAcceptableActionIndustrialProcessingReason { get; init; }

    [JsonPropertyName("notAcceptableActionReDispatchReason")]
    public required DecisionNotAcceptableActionReDispatchReasonEnum NotAcceptableActionReDispatchReason { get; init; }

    [JsonPropertyName("notAcceptableActionUseForOtherPurposesReason")]
    public required DecisionNotAcceptableActionUseForOtherPurposesReasonEnum NotAcceptableActionUseForOtherPurposesReason { get; init; }

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
    public required Party DetailsOfControlledDestinations { get; init; }

    [JsonPropertyName("specificWarehouseNonConformingConsignment")]
    public required DecisionSpecificWarehouseNonConformingConsignmentEnum SpecificWarehouseNonConformingConsignment { get; init; }

    [JsonPropertyName("temporaryDeadline")]
    [Description("Deadline when consignment has to leave borders")]
    public string? TemporaryDeadline { get; init; }

    [JsonPropertyName("decisionEnum")]
    public required DecisionDecisionEnum DecisionEnum { get; init; }

    [JsonPropertyName("freeCirculationPurpose")]
    public required DecisionFreeCirculationPurposeEnum FreeCirculationPurpose { get; init; }

    [JsonPropertyName("definitiveImportPurpose")]
    public required DecisionDefinitiveImportPurposeEnum DefinitiveImportPurpose { get; init; }

    [JsonPropertyName("ifChanneledOption")]
    public required DecisionIfChanneledOptionEnum IfChanneledOption { get; init; }

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
