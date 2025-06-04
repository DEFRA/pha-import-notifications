using System.Net;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Api.Services;

public class TradeImportsDataApiService(TradeImportsDataApiHttpClient tradeImportsDataApiHttpClient)
    : ITradeImportsDataApiService
{
    public async Task<IEnumerable<ImportNotificationUpdate>> GetImportNotificationUpdates(
        string[] importNotificationTypes,
        string[] bcp,
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken
    )
    {
        var qb = new QueryBuilder()
        {
            { "type", importNotificationTypes },
            { "pointOfEntry", bcp },
            { "excludeStatus", "DRAFT" },
            { "from", from.ToString("O") },
            { "to", to.ToString("O") },
        };
        var response =
            await tradeImportsDataApiHttpClient.Client.GetFromJsonAsync<ImportPreNotificationUpdatesResponse>(
                TradeImportsDataApiHttpClient.Endpoints.ImportNotificationUpdates() + qb,
                cancellationToken
            );

        return response?.ImportPreNotificationUpdates.Select(u => u.ToImportNotificationUpdate()) ?? [];
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
                await tradeImportsDataApiHttpClient.Client.GetFromJsonAsync<ImportPreNotificationResponse>(
                    TradeImportsDataApiHttpClient.Endpoints.ImportNotification(chedReferenceNumber),
                    cancellationToken: cancellationToken
                );

            importNotification = importPreNotificationResponse!.ImportPreNotification;
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        var getCustomsDeclarations = tradeImportsDataApiHttpClient.Client.GetFromJsonAsync<CustomsDeclarationsResponse>(
            TradeImportsDataApiHttpClient.Endpoints.CustomsDeclarations(chedReferenceNumber),
            cancellationToken: cancellationToken
        );

        var getGoodsMovements = tradeImportsDataApiHttpClient.Client.GetFromJsonAsync<GmrsResponse>(
            TradeImportsDataApiHttpClient.Endpoints.GoodsMovements(chedReferenceNumber),
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
}
