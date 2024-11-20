using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

public class CdmsService(HttpClient httpClient) : ICdmsService
{
    public async Task<IReadOnlyList<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync("api/import-notifications", cancellationToken);

        response.EnsureSuccessStatusCode();

        return new List<ImportNotification>();
    }
}