namespace Defra.PhaImportNotifications.Contracts;
public enum FinalState
{
    Cleared,
    CancelledAfterArrival,
    CancelledWhilePreLodged,
    Destroyed,
    Seized,
    ReleasedToKingsWarehouse,
    TransferredToMss
}
