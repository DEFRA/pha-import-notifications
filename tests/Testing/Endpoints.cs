using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string GetUpdatedValid(string[]? bcp = null, string? from = null, string? to = null) =>
            GetUpdated(bcp ?? ["bcp"], from ?? "2024-12-11T13:00:00Z", to ?? "2024-12-11T13:30:00Z");

        public static string GetUpdated(string[]? bcp = null, string? from = null, string? to = null)
        {
            var query = new QueryBuilder();

            if (bcp is not null)
            {
                foreach (var se in bcp)
                {
                    query.Add("bcp", se);
                }
            }

            if (from is not null)
                query.Add("from", from);

            if (to is not null)
                query.Add("to", to);

            return $"{Root}{query}";
        }

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";
    }
}
