namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string GetUpdated(DateTime from, DateTime to, string[]? bcp = null)
        {
            string? bcpParam = null;

            if (bcp is not null && bcp.Length > 0)
            {
                bcpParam = string.Join("&", bcp.Select(x => $"bcp={x}")) + "&";
            }

            return $"{Root}?{bcpParam}from={IsoDate(from)}&to={IsoDate(to)}";
        }

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";

        private static string IsoDate(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm");
    }
}
