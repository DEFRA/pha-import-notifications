namespace Defra.PhaImportNotifications.Api.JsonApi;

public static class JsonApiClientExtensions
{
    public static IEnumerable<T> GetDataAsList<T>(this Document document)
    {
        return document.Data.ManyValue is null ? [] : JsonApiClient.GetResourcesAs<T>(document.Data.ManyValue);
    }

    public static T? GetDataAs<T>(this Document document)
    {
        return document.Data.SingleValue is null
            ? default
            : JsonApiClient.GetResourcesAs<T>(new List<ResourceObject> { document.Data.SingleValue }).FirstOrDefault();
    }

    public static IEnumerable<T> GetIncludedAsList<T>(this Document document, string type, int id) =>
        document.GetIncludedAsList<T>(type, id.ToString());

    public static IEnumerable<T> GetIncludedAsList<T>(this Document document, string type, string id)
    {
        var relationships = document.GetRelationships(type, id);

        var resources =
            document
                .Included?.Where(included =>
                    Equals(included.Type, type)
                    && relationships.Any(y => Equals(y.Type, type) && Equals(y.Id, included.Id))
                )
                .ToList() ?? [];

        return JsonApiClient.GetResourcesAs<T>(resources);
    }

    private static IEnumerable<Relationship> GetRelationships(this Document document, string type, string id)
    {
        var resources = document.Data.ManyValue ?? [];

        if (document.Data.SingleValue is not null)
            resources = new List<ResourceObject> { document.Data.SingleValue };

        var resource = resources.FirstOrDefault(x => Equals(x.Id, id));

        if (resource is null)
            return [];

        return resource
                .Relationships?.FirstOrDefault(x => Equals(x.Key, type))
                .Value?.Data.ManyValue?.Where(x => x is { Type: not null, Id: not null })
                .Select(x => new Relationship(x.Type!, x.Id!)) ?? [];
    }

    private static bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
}
