namespace Defra.PhaImportNotifications.Contracts.UpdatedImportNotifications;

public class UpdatedImportNotification
{
    public ImportNotificationLinks Links { get; set; } = new();
}

public class ImportNotificationLinks
{
    public Uri ImportNotification { get; set; }
}
