namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string GetUpdated(string[]? bcp = null)
        {
            string? bcpParam = null;

            if (bcp is not null && bcp.Length > 0)
            {
                bcpParam = string.Join("&", bcp.Select(x => $"bcp={x}")) + "&";
            }

            return $"{Root}?{bcpParam}from=2024-11-20";
        }

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";
    }
}
