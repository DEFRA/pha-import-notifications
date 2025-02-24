namespace Defra.PhaImportNotifications.Contracts;
public enum DecisionStatusEnum
{
    BtmsMadeSameDecisionAsAlvs,
    BtmMadeSameDecisionTypeAsAlvs,
    NoImportNotificationsLinked,
    PartialImportNotificationsLinked,
    NoAlvsDecisions,
    DocumentReferenceFormatIncorrect,
    DocumentReferenceCaseIncorrect,
    AlvsX00NotBtms,
    ReliesOnCDMS205,
    ReliesOnCDMS249,
    HasChedppChecks,
    HasOtherDataErrors,
    HasGenericDataErrors,
    HasMultipleChedTypes,
    HasMultipleCheds,
    BtmsClearAlvsHold,
    AlvsClearBtmsHold,
    InvestigationNeeded,
    None
}
