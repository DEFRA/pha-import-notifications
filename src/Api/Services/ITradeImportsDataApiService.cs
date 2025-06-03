using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public interface ITradeImportsDataApiService
{
    Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] bcp,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    );
    Task<ImportPreNotification?> GetImportNotification(string chedReferenceNumber, CancellationToken cancellationToken);
}
