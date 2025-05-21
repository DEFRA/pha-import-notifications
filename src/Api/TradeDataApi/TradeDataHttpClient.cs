namespace Defra.PhaImportNotifications.Api.TradeDataApi;

public class TradeDataHttpClient(HttpClient client)
{
    public HttpClient Client { get; } = client;

    public static class Endpoints
    {
        private const string Path = "import-pre-notifications";

        public static string ImportNotification(string chedReferenceNumber) => $"/{Path}/{chedReferenceNumber}";

        public static string CustomsDeclarations(string chedReferenceNumber) =>
            $"{ImportNotification(chedReferenceNumber)}/customs-declarations";

        public static string GoodsMovements(string chedReferenceNumber) =>
            $"{ImportNotification(chedReferenceNumber)}/gmrs";
    }
}
