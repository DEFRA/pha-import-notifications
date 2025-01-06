using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Extensions;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.Extensions.Options;
using Document = Defra.PhaImportNotifications.Api.JsonApi.Document;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(IJsonApiClient jsonApiClient, IOptions<BtmsOptions> btmsOptions) : IBtmsService
{
    public async Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] bcp,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    )
    {
        var filter = new FilterExpression(
            LogicalOperator.And,
            [
                new AnyExpression("_PointOfEntry", bcp),
                new AnyExpression("importNotificationType", Enum.GetNames<ImportNotificationTypeEnum>()),
                new NotExpression(new ComparisonExpression(ComparisonOperator.Equals, "status", "Draft")),
                new ComparisonExpression(ComparisonOperator.GreaterOrEqual, "updated", from.ToString("O")),
                new ComparisonExpression(ComparisonOperator.LessThan, "updated", to.ToString("O")),
            ]
        );
        var fields = new[] { new FieldExpression("import-notifications", ["updated", "referenceNumber"]) };

        var document = await jsonApiClient.Get(
            new RequestUri("api/import-notifications", filter, fields, btmsOptions.Value.PageSize),
            cancellationToken
        );

        if (document is null)
            throw new InvalidOperationException("Result was null");

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

        if (document is null)
            return null;

        var tasks = document
            .GetRelationships(chedReferenceNumber, "movements")
            .Select(x => jsonApiClient.Get(new RequestUri($"api/movements/{x.Id}"), cancellationToken));

        var clearanceRequests = (await Task.WhenAll(tasks))
            .ThrowIfAnyNull("At least one movement could not be found")
            .Select(x => x.GetDataAs<Movement>())
            .ThrowIfAnyNull("At least one movement could not be deserialized")
            .SelectMany(x => x.ClearanceRequests ?? []);

        var result = document.GetDataAs<ImportNotification>();
        if (result is null)
            throw new InvalidOperationException("Result was null");

        return result with
        {
            ClearanceRequests = clearanceRequests.ToList(),
        };
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
        var nextRequestUri = document.Links?.Next;

        while (!string.IsNullOrEmpty(nextRequestUri))
        {
            var next = await jsonApiClient.Get(nextRequestUri, cancellationToken);

            if (next is null)
                throw new InvalidOperationException("Next page was null");

            results.AddRange(next.GetDataAsList<ImportNotificationUpdate>());

            nextRequestUri = next.Links?.Next;
        }

        return results;
    }
}
