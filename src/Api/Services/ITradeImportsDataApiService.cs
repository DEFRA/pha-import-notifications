using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public interface ITradeImportsDataApiService
{
    Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] importNotificationTypes,
        string[] bcp,
        DateTime from,
        DateTime to,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    );
    Task<ImportPreNotification?> GetImportNotification(string chedReferenceNumber, CancellationToken cancellationToken);
}
