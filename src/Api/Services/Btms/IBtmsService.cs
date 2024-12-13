using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public interface IBtmsService
{
    Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] bcp,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    );
    Task<ImportNotification?> GetImportNotification(string chedReferenceNumber, CancellationToken cancellationToken);
}
