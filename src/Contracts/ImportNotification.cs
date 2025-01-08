namespace Defra.PhaImportNotifications.Contracts;

public partial record ImportNotification
{
    public IReadOnlyList<CustomsClearanceRequest>? ClearanceRequests { get; init; }

    public IReadOnlyList<CustomsClearanceRequest>? ClearanceDecisions { get; init; }
}
