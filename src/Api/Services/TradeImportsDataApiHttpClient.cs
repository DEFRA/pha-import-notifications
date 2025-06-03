namespace Defra.PhaImportNotifications.Api.Services;

public class TradeImportsDataApiHttpClient(HttpClient client)
{
    public HttpClient Client { get; } = client;

    public static class Endpoints
    {
        private const string ImportPreNotificationPath = "import-pre-notifications";

        private const string UpdatePath = "import-pre-notification-updates";

        public static string ImportNotificationUpdates() => $"/{UpdatePath}";

        public static string ImportNotification(string chedReferenceNumber) =>
            $"/{ImportPreNotificationPath}/{chedReferenceNumber}";

        public static string CustomsDeclarations(string chedReferenceNumber) =>
            $"{ImportNotification(chedReferenceNumber)}/customs-declarations";

        public static string GoodsMovements(string chedReferenceNumber) =>
            $"{ImportNotification(chedReferenceNumber)}/gmrs";
    }
}
