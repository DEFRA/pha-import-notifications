using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

internal static class TradeImportsDataApiMappingExtensions
{
    public static ImportNotificationUpdatesPaged ToImportNotificationUpdatesPaged(
        this ImportPreNotificationUpdatesResponse response
    )
    {
        return new ImportNotificationUpdatesPaged
        {
            ImportNotifications = response.ImportPreNotificationUpdates.Select(u => u.ToImportNotificationUpdate()),
            Page = response.Page,
            PageSize = response.PageSize,
            Total = response.Total,
        };
    }

    public static ImportNotificationUpdate ToImportNotificationUpdate(this ImportPreNotificationUpdateResponse response)
    {
        return new ImportNotificationUpdate
        {
            ReferenceNumber = response.ReferenceNumber,
            UpdatedEntity = response.Updated,
        };
    }
}
