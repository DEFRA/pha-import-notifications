using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string Get(string chedReferenceNumber) => $"/import-notifications/{chedReferenceNumber}";

        public static string GetUpdatedValid(
            string[]? bcp = null,
            string from = "2024-12-11T13:00:00Z",
            string to = "2024-12-11T13:30:00Z"
        ) => GetUpdatedBetween(bcp, from, to);

        public static string GetUpdatedBetween(string[]? bcp, string from, string to)
        {
            var query = new QueryBuilder();

            if (bcp is not null)
                foreach (var se in bcp)
                    query.Add("bcp", se);

            query.Add("from", from);

            query.Add("to", to);

            return $"{Root}{query}";
        }
    }
}
