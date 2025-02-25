namespace Defra.PhaImportNotifications.Contracts;
public enum BusinessDecisionStatusEnum
{
    CancelledOrDestroyed,
    ManualReleases,
    AlvsDataErrorDecision,
    BtmsDataErrorDecision,
    MatchComplete,
    MatchGroup,
    AlvsHoldBtmsNotHeld,
    AlvsNotHeldBtmsHold,
    AlvsReleaseBtmsNotReleased,
    AlvsNotReleasedBtmsReleased,
    AlvsRefuseBtmsNotRefused,
    AlvsNotRefusedBtmsRefused,
    AnythingElse
}
