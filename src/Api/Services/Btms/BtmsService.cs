using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(HttpClient httpClient) : IBtmsService
{
    private readonly JsonApiClient _jsonApiClient = new(httpClient);

    public async Task<IEnumerable<ImportNotification>> GetImportNotifications(CancellationToken cancellationToken)
    {
        var result = await _jsonApiClient.Get("api/import-notifications", cancellationToken);

        return JsonApiClient.GetResourceList<ImportNotification>(result);
    }

    public Task<ImportNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}
