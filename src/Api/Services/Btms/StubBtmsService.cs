using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

[ExcludeFromCodeCoverage]
public class StubBtmsService : IBtmsService
{
    public Task<IEnumerable<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken) =>
        Task.FromResult<IEnumerable<ImportNotification>>(
            new List<ImportNotification>
            {
                new()
                {
                    ReferenceNumber = "CHED1234",
                    LastUpdated = new DateTime(2024, 11, 24, 23, 59, 30, DateTimeKind.Utc),
                },
                new()
                {
                    ReferenceNumber = "CHED5678",
                    LastUpdated = new DateTime(2024, 11, 24, 23, 59, 59, DateTimeKind.Utc),
                },
            }
        );

    public Task<ImportNotification?> GetImportNotification(
        string referenceNumber,
        CancellationToken cancellationToken
    ) => Task.FromResult<ImportNotification?>(new ImportNotification { ReferenceNumber = referenceNumber });
}
