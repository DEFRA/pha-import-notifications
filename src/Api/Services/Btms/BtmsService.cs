using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(HttpClient httpClient) : IBtmsService
{
    public async Task<IEnumerable<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync("api/import-notifications", cancellationToken);

        response.EnsureSuccessStatusCode();

        return new List<ImportNotification>();
    }

    public Task<ImportNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}
