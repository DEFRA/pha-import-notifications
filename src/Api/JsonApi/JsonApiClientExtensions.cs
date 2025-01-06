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

    /// <summary>
    /// Get any included data within the specified document, looking for the primary resource by
    /// id, which will then look at any relationships by resourceType.
    /// </summary>
    /// <param name="document">The single or many item result document that will contain the resource by id</param>
    /// <param name="id">The id of the resource within the document</param>
    /// <param name="resourceType">The type of relationship that will be inspected once the resource has been found</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A list of any included data that matched by relationship from the resource</returns>
    public static IEnumerable<T> GetIncludedAsList<T>(this Document document, int id, string resourceType) =>
        document.GetIncludedAsList<T>(id.ToString(), resourceType);

    private static List<T> GetIncludedAsList<T>(this Document document, string id, string resourceType)
    {
        var relationships = document.GetRelationships(id, resourceType);

        var resources =
            document
                .Included?.Where(included =>
                    Equals(included.Type, resourceType)
                    && relationships.Any(y => Equals(y.ResourceType, resourceType) && Equals(y.Id, included.Id))
                )
                .ToList() ?? [];

        return JsonApiClient.GetResourcesAs<T>(resources);
    }

    /// <summary>
    /// Get any relationships within the specified document, looking for the primary resource by
    /// id, which will then look at any relationships by resourceType.
    /// </summary>
    /// <param name="document">The single or many item result document that will contain the resource by id</param>
    /// <param name="id">The id of the resource within the document</param>
    /// <param name="resourceType">The type of relationship that will be returned</param>
    /// <returns>A list of any matching relationships</returns>
    public static IEnumerable<Relationship> GetRelationships(this Document document, string id, string resourceType)
    {
        var resources = document.Data.ManyValue ?? [];

        if (document.Data.SingleValue is not null)
            resources = new List<ResourceObject> { document.Data.SingleValue };

        var resource = resources.FirstOrDefault(x => Equals(x.Id, id));

        if (resource is null)
            return [];

        return resource
                .Relationships?.FirstOrDefault(x => Equals(x.Key, resourceType))
                .Value?.Data.ManyValue?.Where(x => x is { Type: not null, Id: not null })
                .Select(x => new Relationship(x.Type!, x.Id!)) ?? [];
    }

    private static bool Equals(string? x, string? y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
}
