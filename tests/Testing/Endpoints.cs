using System.Globalization;

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

        private static string IsoDate(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm");

        public static string Get(DateTime from, DateTime to) => $"{Root}/pha?from={IsoDate(from)}&to={IsoDate(to)}";
    }
}
