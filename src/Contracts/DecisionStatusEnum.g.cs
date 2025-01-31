namespace Defra.PhaImportNotifications.Contracts;
public enum DecisionStatusEnum
{
    BtmsMadeSameDecisionAsAlvs,
    ReliesOnCDMS205,
    ReliesOnCDMS249,
    HasChedppChecks,
    NoImportNotificationsLinked,
    HasOtherDataErrors,
    HasGenericDataErrors,
    NoAlvsDecisions,
    HasMultipleChedTypes,
    HasMultipleCheds,
    InvestigationNeeded,
    None
}
