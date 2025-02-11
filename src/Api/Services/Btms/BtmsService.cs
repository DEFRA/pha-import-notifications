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
                new ComparisonExpression(ComparisonOperator.GreaterOrEqual, "updatedEntity", from.ToString("O")),
                new ComparisonExpression(ComparisonOperator.LessThan, "updatedEntity", to.ToString("O")),
            ]
        );
        var fields = new[] { new FieldExpression("import-notifications", ["updatedEntity", "referenceNumber"]) };

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

        async Task<List<TType>> GetRelated<TType>(string type)
        {
            var relationships = document.GetRelationships(chedReferenceNumber, type);

            var tasks = relationships.Select(x =>
                jsonApiClient.Get(new RequestUri($"api/{type}/{x.Id}"), cancellationToken)
            );

            return (await Task.WhenAll(tasks))
                .ThrowIfAnyNull($"At least one {type} could not be found")
                .Select(x => x.GetDataAs<TType>())
                .ThrowIfAnyNull($"At least one {type} could not be deserialized")
                .ToList();
        }

        var movements = await GetRelated<Movement>("movements");
        var goodsMovements = await GetRelated<Gmr>("gmrs");

        var result = document.GetDataAs<ImportNotification>();
        if (result is null)
            throw new InvalidOperationException("Result was null");

        return result with
        {
            ClearanceRequests = movements.SelectMany(x => x.ClearanceRequests ?? []).ToList(),
            ClearanceDecisions = movements.SelectMany(x => x.Decisions ?? []).ToList(),
            GoodsMovements = goodsMovements,
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
