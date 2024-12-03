using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

[ExcludeFromCodeCoverage]
public class StubBtmsService : IBtmsService
{
    public Task<IEnumerable<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<ImportNotification>>(new List<ImportNotification>());
    }

    public Task<ImportNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult<ImportNotification?>(null);
    }
}
