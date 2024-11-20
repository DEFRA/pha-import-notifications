using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public interface ICdmsService
{
    Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken);
}
