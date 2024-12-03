namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";
    }

    public static class ImportNotificationsUpdates
    {
        private const string Root = "/import-notifications-updates";

        public static string Get() => $"{Root}/pha?from=2024-11-20";
    }
}
