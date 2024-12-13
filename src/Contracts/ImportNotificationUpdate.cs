namespace Defra.PhaImportNotifications.Contracts;

public class ImportNotificationUpdate
{
    public required DateTime Updated { get; init; }
    public required string ReferenceNumber { get; init; }
}
