using System.Net;
using System.Net.Http.Headers;
using Defra.PhaImportNotifications.Api.JsonApi.Configuration;
using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddJsonApiClient(
        this IServiceCollection services,
        Action<IServiceProvider, JsonApiClientOptions> configure
    )
    {
        services.AddTransient<IConfigureOptions<JsonApiClientOptions>>(sp => new ConfigureOptions<JsonApiClientOptions>(
            options => configure(sp, options)
        ));

        return services
            .AddHttpClient<IJsonApiClient, JsonApiClient>(
                (sp, httpClient) =>
                {
                    var options = sp.GetRequiredService<IOptions<JsonApiClientOptions>>().Value;

                    httpClient.BaseAddress = new Uri(options.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/vnd.api+json")
                    );

                    if (options.BasicAuthCredential is not null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            options.BasicAuthCredential
                        );
                    }

                    if (httpClient.BaseAddress.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                        httpClient.DefaultRequestVersion = HttpVersion.Version20;
                }
            )
            .AddHeaderPropagation();
    }
}
