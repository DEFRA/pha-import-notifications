using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public interface IBtmsService
{
    Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken);
}
