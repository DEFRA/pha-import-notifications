using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(JsonApiClient jsonApiClient) : IBtmsService
{
    public async Task<IEnumerable<ImportNotification>> GetImportNotifications(
        string[] bcp,
        CancellationToken cancellationToken
    )
    {
        var filter = new FilterExpression(
            LogicalOperator.Or,
            bcp.Select(x => new ComparisonExpression(ComparisonOperator.Equals, "_PointOfEntry", x)).ToList()
        );
        var document = await jsonApiClient.Get("api/import-notifications", cancellationToken, filter);

        // This may return a document with errors so we need to check things like this when we integrate.
        // It could also throw and do all the usual stuff.

        return document.GetDataAsList<ImportNotification>();
    }

    public async Task<ImportNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    )
    {
        var document = await jsonApiClient.Get($"api/import-notifications/{chedReferenceNumber}", cancellationToken);

        // This may return a document with errors so we need to check things like this when we integrate.
        // It could also throw and do all the usual stuff.

        return document.GetDataAs<ImportNotification>();
    }
}
