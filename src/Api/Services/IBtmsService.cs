using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public interface IBtmsService
{
    Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken);
    Task<ImportNotification?> GetImportNotification(string referenceNumber, CancellationToken cancellationToken);
}
