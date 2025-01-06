using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public class JsonApiClient(HttpClient httpClient, ILogger<JsonApiClient> logger) : IJsonApiClient
{
    private static readonly JsonSerializerOptions s_options = new(JsonSerializerDefaults.Web)
    {
        Converters = { new SingleOrManyDataConverterFactory(), new JsonStringEnumConverter() },
    };

    public Task<Document?> Get(RequestUri requestUri, CancellationToken cancellationToken) =>
        Get(requestUri.ToString(), cancellationToken);

    public async Task<Document?> Get(string requestUri, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        using var response = await httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        if (response.StatusCode is HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
        {
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync(cancellationToken);

            logger.LogWarning(
                "Request failed {RequestUri} {StatusCode} {Content}",
                requestUri,
                response.StatusCode,
                content
            );
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Document>(s_options, cancellationToken);
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
