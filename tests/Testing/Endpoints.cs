using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Testing;

public static class Endpoints
{
    public static class ImportNotifications
    {
        private const string Root = "/import-notifications";

        public static string GetUpdated(DateTime from, DateTime to, string[]? bcp = null)
        {
            var query = new QueryBuilder();

            if (bcp is not null)
            {
                foreach (var se in bcp)
                {
                    query.Add("bcp", se);
                }
            }

            query.Add("from", from.ToString("o"));
            query.Add("to", to.ToString("o"));

            return $"{Root}{query}";
        }

        public static string Get(string chedReferenceNumber = ChedReferenceNumbers.ChedA) =>
            $"{Root}/{chedReferenceNumber}";
    }
}
