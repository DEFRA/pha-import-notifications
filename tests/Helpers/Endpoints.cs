using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Tests.Helpers;

public static class Endpoints
{
    private const string Root = "/import-notifications";

    public class UpdateUrlBuilder
    {
        private readonly QueryBuilder _query = [];

        public UpdateUrlBuilder WithValidPeriod() => WithFrom("2024-12-11T13:00:00Z").WithTo("2024-12-11T13:30:00Z");

        public UpdateUrlBuilder WithPeriod(DateTime from, DateTime to) => WithFrom(from).WithTo(to);

        public UpdateUrlBuilder WithFrom(DateTime from) => WithFrom(from.ToString("O"));

        public UpdateUrlBuilder WithFrom(string from)
        {
            _query.Add("from", from);
            return this;
        }

        public UpdateUrlBuilder WithTo(DateTime to) => WithTo(to.ToString("O"));

        public UpdateUrlBuilder WithTo(string to)
        {
            _query.Add("to", to);
            return this;
        }

        public UpdateUrlBuilder WithBcp(params string[]? bcps)
        {
            if (bcps is not null)
            {
                foreach (var se in bcps)
                    _query.Add("bcp", se);
            }

            return this;
        }

        public UpdateUrlBuilder WithChedType(params string[]? chedTypes)
        {
            if (chedTypes is not null)
            {
                foreach (var se in chedTypes)
                    _query.Add("chedType", se);
            }

            return this;
        }

        public static string GetUpdatedValid() => new UpdateUrlBuilder().WithValidPeriod().Build();

        public string Build() => $"{Root}{_query}";
    }

    public static class ImportNotifications
    {
        public static string Get(string chedReferenceNumber) => $"/import-notifications/{chedReferenceNumber}";
    }
}
