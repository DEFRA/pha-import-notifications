using System.Net;
using System.Reflection;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.TradeDataApi;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.Extensions.Options;
using Document = Defra.PhaImportNotifications.Api.JsonApi.Document;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class BtmsService(
    IJsonApiClient jsonApiClient,
    TradeDataHttpClient tradeDataHttpClient,
    IOptions<BtmsOptions> btmsOptions
) : ITradeImportsDataService
{
    private static readonly string[] importNotificationTypes = typeof(ImportPreNotification)
        .GetProperty(nameof(ImportPreNotification.ImportNotificationType))!
        .GetCustomAttributes<ExampleValueAttribute>()
        .Select(v => v.Value)
        .ToArray();

    public async Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] bcp,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<IExpression> bcpFilter = bcp.Length > 0 ? [new AnyExpression("_PointOfEntry", bcp)] : [];
        var filters = bcpFilter.Concat(
            [
                new AnyExpression("importNotificationType", importNotificationTypes),
                new NotExpression(new ComparisonExpression(ComparisonOperator.Equals, "status", "Draft")),
                new ComparisonExpression(ComparisonOperator.GreaterOrEqual, "updatedEntity", from.ToString("O")),
                new ComparisonExpression(ComparisonOperator.LessThan, "updatedEntity", to.ToString("O")),
            ]
        );

        var filterExpression = new FilterExpression(LogicalOperator.And, filters);
        var fields = new[] { new FieldExpression("import-notifications", ["updatedEntity", "referenceNumber"]) };

        var document = await jsonApiClient.Get(
            new RequestUri("api/import-notifications", filterExpression, fields, btmsOptions.Value.PageSize),
            cancellationToken
        );

        if (document is null)
            throw new InvalidOperationException("Result was null");

        return await CollectImportNotificationUpdatesFromAllPages(document, cancellationToken);
    }

    public async Task<ImportPreNotification?> GetImportNotification(
        string chedReferenceNumber,
        CancellationToken cancellationToken
    )
    {
        ImportPreNotification importNotification;

        try
        {
            var importPreNotificationResponse =
                await tradeDataHttpClient.Client.GetFromJsonAsync<ImportPreNotificationResponse>(
                    TradeDataHttpClient.Endpoints.ImportNotification(chedReferenceNumber),
                    cancellationToken: cancellationToken
                );

            importNotification = importPreNotificationResponse!.ImportPreNotification;
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var getCustomsDeclarations = tradeDataHttpClient.Client.GetFromJsonAsync<CustomsDeclarationsResponse>(
            TradeDataHttpClient.Endpoints.CustomsDeclarations(chedReferenceNumber),
            cancellationToken: cancellationToken
        );

        var getGoodsMovements = tradeDataHttpClient.Client.GetFromJsonAsync<GmrsResponse>(
            TradeDataHttpClient.Endpoints.GoodsMovements(chedReferenceNumber),
            cancellationToken: cancellationToken
        );

        var customsDeclarationsResponse = await getCustomsDeclarations;
        var goodsMovementsResponse = await getGoodsMovements;

        importNotification.CustomsDeclarations = customsDeclarationsResponse!
            .CustomsDeclarations.Select(cdr => new CustomsDeclarationData
            {
                MovementReferenceNumber = cdr.MovementReferenceNumber,
                ClearanceRequest = cdr.ClearanceRequest,
                ClearanceDecision = cdr.ClearanceDecision,
                Finalisation = cdr.Finalisation,
            })
            .ToList();

        importNotification.GoodsMovements = goodsMovementsResponse!.Gmrs.Select(g => g.Gmr).ToList();

        return importNotification;
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
