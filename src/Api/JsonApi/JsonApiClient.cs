using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public class JsonApiClient(HttpClient httpClient, ILogger<JsonApiClient> logger)
{
    private static readonly JsonSerializerOptions s_options = new(JsonSerializerDefaults.Web)
    {
        Converters = { new SingleOrManyDataConverterFactory(), new JsonStringEnumConverter() },
    };

    public async Task<Document> Get(string requestUri, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        using var response = await httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        var result = await response.Content.ReadFromJsonAsync<Document>(s_options, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Request failed {StatusCode} {Document}", response.StatusCode, result);
        }

        if (result is null)
            throw new InvalidOperationException("Could not deserialize JSON");

        return result;
    }

    public static List<T> GetResourcesAs<T>(IList<ResourceObject> resources)
    {
        var results = new List<T>();

        foreach (var data in resources)
        {
            var attributes = data.Attributes.ToDictionary();
            attributes.Add(nameof(data.Id), data.Id);

            var resource = JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(attributes, s_options), s_options);

            if (resource is not null)
                results.Add(resource);
        }

        return results;
    }
}
