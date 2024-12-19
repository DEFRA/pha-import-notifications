namespace Defra.PhaImportNotifications.Contracts;

public partial class ImportNotification
{
    public List<CustomsClearanceRequest>? ClearanceRequests { get; init; }
}
