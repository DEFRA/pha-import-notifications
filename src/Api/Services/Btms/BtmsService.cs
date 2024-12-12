using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(JsonApiClient jsonApiClient) : IBtmsService
{
    public async Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] bcp,
        CancellationToken cancellationToken
    )
    {
        var filter = new FilterExpression(
            LogicalOperator.And,
            [
                new AnyExpression("_PointOfEntry", bcp),
                new AnyExpression("importNotificationType", Enum.GetNames<ImportNotificationTypeEnum>()),
                new NotExpression(new ComparisonExpression(ComparisonOperator.Equals, "status", "Draft")),
            ]
        );
        var fields = new[] { new FieldExpression("import-notifications", ["updated", "referenceNumber"]) };
        var document = await jsonApiClient.Get(
            new RequestUri("api/import-notifications", filter, fields),
            cancellationToken
        );

        // This may return a document with errors so we need to check things like this when we integrate.
        // It could also throw and do all the usual stuff.

        return await CollectImportNotificationUpdatesFromAllPages(document, cancellationToken);
    }

    public async Task<ImportNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    )
    {
        var document = await jsonApiClient.Get(
            new RequestUri($"api/import-notifications/{chedReferenceNumber}"),
            cancellationToken
        );

        // This may return a document with errors so we need to check things like this when we integrate.
        // It could also throw and do all the usual stuff.

        return document.GetDataAs<ImportNotification>();
    }

    /// <summary>
    /// This method is knowingly sequential currently. Given the behaviour of a JSON.API we need
    /// to make a request to then be told if there are more results via the Next link. If there are,
    /// we request the data, and so on. If we preempted the pagination and used the total results
    /// number in the metadata of the response, we could retrieve data in parallel and improve
    /// performance. This has not been done yet.
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<IList<ImportNotificationUpdate>> CollectImportNotificationUpdatesFromAllPages(
        Document document,
        CancellationToken cancellationToken
    )
    {
        var results = new List<ImportNotificationUpdate>(document.GetDataAsList<ImportNotificationUpdate>());

        while (!string.IsNullOrEmpty(document.Links?.Next))
        {
            document = await jsonApiClient.Get(document.Links.Next, cancellationToken);

            results.AddRange(document.GetDataAsList<ImportNotificationUpdate>());
        }

        return results;
    }
}
