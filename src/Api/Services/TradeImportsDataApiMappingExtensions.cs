using System.Runtime.CompilerServices;
using Defra.PhaImportNotifications.Contracts;

namespace Defra.PhaImportNotifications.Api.Services;

internal static class TradeImportsDataApiMappingExtensions
{
    public static ImportNotificationUpdate ToImportNotificationUpdate(this ImportPreNotificationUpdateResponse response)
    {
        return new ImportNotificationUpdate
        {
            ReferenceNumber = response.ReferenceNumber,
            UpdatedEntity = response.Updated,
        };
    }
}
