using Microsoft.AspNetCore.Http.Extensions;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public record RequestUri(
    string Path,
    FilterExpression? Filter = null,
    FieldExpression[]? Fields = null,
    int? PageSize = null
)
{
    public override string ToString()
    {
        var query = new QueryBuilder();

        if (Filter is not null)
        {
            query.Add("filter", Filter.ToString());
        }

        foreach (var field in Fields ?? [])
        {
            var parts = field.ToParts();

            if (parts is null)
                continue;

            query.Add(parts.Value.Key, parts.Value.Value);
        }

        if (PageSize is not null)
        {
            query.Add("page[size]", PageSize.Value.ToString());
        }

        return $"{Path}{query}";
    }
}
