using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

[ExcludeFromCodeCoverage]
public class StubBtmsService : IBtmsService
{
    public Task<IEnumerable<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken) =>
        Task.FromResult<IEnumerable<ImportNotification>>(new List<ImportNotification> { Capacity = 0 });

    public Task<ImportNotification?> GetImportNotification(
        string referenceNumber,
        CancellationToken cancellationToken
    ) => Task.FromResult<ImportNotification?>(null);
}
