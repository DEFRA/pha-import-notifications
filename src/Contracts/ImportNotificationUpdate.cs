namespace Defra.PhaImportNotifications.Contracts;

public class ImportNotificationUpdate
{
    public required DateTime UpdatedEntity { get; init; }
    public required string ReferenceNumber { get; init; }
}
