using System.Net.Http.Headers;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddJsonApiClient(
        this IServiceCollection services,
        Func<IServiceProvider, string> baseUrlFactory
    )
    {
        return services.AddHttpClient<JsonApiClient>(
            (sp, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(baseUrlFactory(sp));
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.api+json")
                );
            }
        );
    }
}
