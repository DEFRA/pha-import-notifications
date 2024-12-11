using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string GetUpdated(DateTime? from = null, DateTime? to = null, string[]? bcp = null)
        {
            var query = new QueryBuilder();

            if (bcp is not null)
            {
                foreach (var se in bcp)
                {
                    query.Add("bcp", se);
                }
            }

            query.Add("from", from?.ToString("o") ?? string.Empty);
            query.Add("to", to?.ToString("o") ?? string.Empty);

            return $"{Root}{query}";
        }

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";
    }
}
