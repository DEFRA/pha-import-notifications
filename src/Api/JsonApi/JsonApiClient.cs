using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public class JsonApiClient
{
    private readonly HttpClient _httpClient;

    private static readonly MediaTypeWithQualityHeaderValue s_contentType = new("application/vnd.api+json");

    private static readonly JsonSerializerOptions s_options =
        new(JsonSerializerDefaults.Web)
        {
            Converters = { new SingleOrManyDataConverterFactory(), new JsonStringEnumConverter() },
        };

    public JsonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(s_contentType);
    }

    public async Task<Document> Get(string requestUri, CancellationToken cancellationToken)
    {
        var result = await _httpClient.GetFromJsonAsync<Document>(requestUri, s_options, cancellationToken);

        if (result is null)
            throw new InvalidOperationException("Could not deserialize JSON");

        return result;
    }

    public static T? GetResource<T>(Document document)
    {
        if (document.Data.SingleValue is null)
            return default;

        return JsonSerializer.Deserialize<T>(
            JsonSerializer.Serialize(document.Data.SingleValue.Attributes, s_options),
            s_options
        );
    }

    public static IEnumerable<T> GetResourceList<T>(Document document)
    {
        if (document.Data.ManyValue is null)
            return [];

        return document
            .Data.ManyValue.Select(x => x.Attributes)
            .Where(x => x != null)
            .Select(x => JsonSerializer.Serialize(x, s_options))
            .Select(x => JsonSerializer.Deserialize<T>(x, s_options))
            .Where(x => !Equals(x, default(T)))!;
    }
}
