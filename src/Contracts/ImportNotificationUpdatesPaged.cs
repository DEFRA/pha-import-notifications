namespace Defra.PhaImportNotifications.Contracts;

public class ImportNotificationUpdatesPaged
{
    public required IEnumerable<ImportNotificationUpdate> ImportNotifications { get; init; }

    public required int Total { get; init; }

    public required int Page { get; init; }

    public required int PageSize { get; init; }
}
