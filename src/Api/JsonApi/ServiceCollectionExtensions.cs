using System.Net;
using System.Net.Http.Headers;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddJsonApiClient(
        this IServiceCollection services,
        Func<IServiceProvider, string> baseUrlFactory,
        Func<IServiceProvider, string> basicAuthCredentialFactory
    )
    {
        return services.AddHttpClient<JsonApiClient>(
            (sp, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(baseUrlFactory(sp));
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.api+json")
                );
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    basicAuthCredentialFactory(sp)
                );

                if (httpClient.BaseAddress.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                    httpClient.DefaultRequestVersion = HttpVersion.Version20;
            }
        );
    }
}
