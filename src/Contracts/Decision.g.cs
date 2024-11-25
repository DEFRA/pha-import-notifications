#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Decision
{
    [JsonPropertyName("consignmentAcceptable")]
    [Description("Is consignment acceptable or not")]
    public bool? ConsignmentAcceptable { get; set; }

    [JsonPropertyName("notAcceptableAction")]
    public DecisionNotAcceptableActionEnum NotAcceptableAction { get; set; }

    [JsonPropertyName("notAcceptableActionDestructionReason")]
    public DecisionNotAcceptableActionDestructionReasonEnum NotAcceptableActionDestructionReason { get; set; }

    [JsonPropertyName("notAcceptableActionEntryRefusalReason")]
    public DecisionNotAcceptableActionEntryRefusalReasonEnum NotAcceptableActionEntryRefusalReason { get; set; }

    [JsonPropertyName("notAcceptableActionQuarantineImposedReason")]
    public DecisionNotAcceptableActionQuarantineImposedReasonEnum NotAcceptableActionQuarantineImposedReason { get; set; }

    [JsonPropertyName("notAcceptableActionSpecialTreatmentReason")]
    public DecisionNotAcceptableActionSpecialTreatmentReasonEnum NotAcceptableActionSpecialTreatmentReason { get; set; }

    [JsonPropertyName("notAcceptableActionIndustrialProcessingReason")]
    public DecisionNotAcceptableActionIndustrialProcessingReasonEnum NotAcceptableActionIndustrialProcessingReason { get; set; }

    [JsonPropertyName("notAcceptableActionReDispatchReason")]
    public DecisionNotAcceptableActionReDispatchReasonEnum NotAcceptableActionReDispatchReason { get; set; }

    [JsonPropertyName("notAcceptableActionUseForOtherPurposesReason")]
    public DecisionNotAcceptableActionUseForOtherPurposesReasonEnum NotAcceptableActionUseForOtherPurposesReason { get; set; }

    [JsonPropertyName("notAcceptableDestructionReason")]
    [Description("Filled when notAcceptableAction is equal to destruction")]
    public string? NotAcceptableDestructionReason { get; set; }

    [JsonPropertyName("notAcceptableActionOtherReason")]
    [Description("Filled when notAcceptableAction is equal to other")]
    public string? NotAcceptableActionOtherReason { get; set; }

    [JsonPropertyName("notAcceptableActionByDate")]
    [Description("Filled when consignmentAcceptable is set to false")]
    public string? NotAcceptableActionByDate { get; set; }

    [JsonPropertyName("chedppNotAcceptableReasons")]
    [Description("List of details for individual chedpp not acceptable reasons")]
    public List<ChedppNotAcceptableReason>? ChedppNotAcceptableReasons { get; set; }

    [JsonPropertyName("notAcceptableReasons")]
    [Description("If the consignment was not accepted what was the reason")]
    public List<string>? NotAcceptableReasons { get; set; }

    [JsonPropertyName("notAcceptableCountry")]
    [Description("2 digits ISO code of country (not acceptable country can be empty)")]
    public string? NotAcceptableCountry { get; set; }

    [JsonPropertyName("notAcceptableEstablishment")]
    [Description("Filled if consignmentAcceptable is set to false")]
    public string? NotAcceptableEstablishment { get; set; }

    [JsonPropertyName("notAcceptableOtherReason")]
    [Description("Filled if consignmentAcceptable is set to false")]
    public string? NotAcceptableOtherReason { get; set; }

    [JsonPropertyName("detailsOfControlledDestinations")]
    public Party DetailsOfControlledDestinations { get; set; }

    [JsonPropertyName("specificWarehouseNonConformingConsignment")]
    public DecisionSpecificWarehouseNonConformingConsignmentEnum SpecificWarehouseNonConformingConsignment { get; set; }

    [JsonPropertyName("temporaryDeadline")]
    [Description("Deadline when consignment has to leave borders")]
    public string? TemporaryDeadline { get; set; }

    [JsonPropertyName("decisionEnum")]
    public DecisionDecisionEnum DecisionEnum { get; set; }

    [JsonPropertyName("freeCirculationPurpose")]
    public DecisionFreeCirculationPurposeEnum FreeCirculationPurpose { get; set; }

    [JsonPropertyName("definitiveImportPurpose")]
    public DecisionDefinitiveImportPurposeEnum DefinitiveImportPurpose { get; set; }

    [JsonPropertyName("ifChanneledOption")]
    public DecisionIfChanneledOptionEnum IfChanneledOption { get; set; }

    [JsonPropertyName("customWarehouseRegisteredNumber")]
    [Description("Custom warehouse registered number")]
    public string? CustomWarehouseRegisteredNumber { get; set; }

    [JsonPropertyName("freeWarehouseRegisteredNumber")]
    [Description("Free warehouse registered number")]
    public string? FreeWarehouseRegisteredNumber { get; set; }

    [JsonPropertyName("shipName")]
    [Description("Ship name")]
    public string? ShipName { get; set; }

    [JsonPropertyName("shipPortOfExit")]
    [Description("Port of exit")]
    public string? ShipPortOfExit { get; set; }

    [JsonPropertyName("shipSupplierRegisteredNumber")]
    [Description("Ship supplier registered number")]
    public string? ShipSupplierRegisteredNumber { get; set; }

    [JsonPropertyName("transhipmentBip")]
    [Description("Transhipment BIP")]
    public string? TranshipmentBip { get; set; }

    [JsonPropertyName("transhipmentThirdCountry")]
    [Description("Transhipment third country")]
    public string? TranshipmentThirdCountry { get; set; }

    [JsonPropertyName("transitExitBip")]
    [Description("Transit exit BIP")]
    public string? TransitExitBip { get; set; }

    [JsonPropertyName("transitThirdCountry")]
    [Description("Transit third country")]
    public string? TransitThirdCountry { get; set; }

    [JsonPropertyName("transitDestinationThirdCountry")]
    [Description("Transit destination third country")]
    public string? TransitDestinationThirdCountry { get; set; }

    [JsonPropertyName("temporaryExitBip")]
    [Description("Temporary exit BIP")]
    public string? TemporaryExitBip { get; set; }

    [JsonPropertyName("horseReentry")]
    [Description("Horse re-entry")]
    public string? HorseReentry { get; set; }

    [JsonPropertyName("transhipmentEuOrThirdCountry")]
    [Description("Is it transshipped to EU or third country (values EU / country name)")]
    public string? TranshipmentEuOrThirdCountry { get; set; }
}
