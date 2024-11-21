using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public class BtmsService(HttpClient httpClient) : IBtmsService
{
    public async Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync("api/import-notifications", cancellationToken);

        response.EnsureSuccessStatusCode();

        return new List<ImportNotification>();
    }

    public Task<ImportNotification?> GetImportNotification(
        string referenceNumber,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}
