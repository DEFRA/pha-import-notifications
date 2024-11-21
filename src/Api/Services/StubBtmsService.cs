using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public class StubBtmsService : IBtmsService
{
    public Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken) =>
        Task.FromResult<IReadOnlyList<ImportNotification>>(
            new List<ImportNotification>
            {
                new() { ReferenceNumber = "CHED1234" },
                new() { ReferenceNumber = "CHED5678" },
            }
        );

    public Task<ImportNotification?> GetImportNotification(
        string referenceNumber,
        CancellationToken cancellationToken
    ) => Task.FromResult<ImportNotification?>(new ImportNotification { ReferenceNumber = referenceNumber });
}
