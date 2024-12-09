using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public interface IBtmsService
{
    Task<IEnumerable<ImportNotification>> GetImportNotifications(string[] bcp, CancellationToken cancellationToken);
    Task<ImportNotification?> GetImportNotification(string chedReferenceNumber, CancellationToken cancellationToken);
}
