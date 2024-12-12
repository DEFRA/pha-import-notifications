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

        return document.GetDataAsList<ImportNotificationUpdate>();
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
}
