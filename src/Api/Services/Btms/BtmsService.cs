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
            LogicalOperator.And,
            [
                new AnyExpression("_PointOfEntry", bcp),
                new AnyExpression("importNotificationType", Enum.GetNames<ImportNotificationTypeEnum>()),
                new NotExpression(new ComparisonExpression(ComparisonOperator.Equals, "status", "Draft")),
            ]
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
