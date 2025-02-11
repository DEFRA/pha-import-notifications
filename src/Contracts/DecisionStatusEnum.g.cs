namespace Defra.PhaImportNotifications.Contracts;
public enum DecisionStatusEnum
{
    BtmsMadeSameDecisionAsAlvs,
    BtmMadeSameDecisionTypeAsAlvs,
    NoImportNotificationsLinked,
    NoAlvsDecisions,
    ReliesOnCDMS205,
    ReliesOnCDMS249,
    HasChedppChecks,
    HasOtherDataErrors,
    HasGenericDataErrors,
    HasMultipleChedTypes,
    HasMultipleCheds,
    InvestigationNeeded,
    None
}
