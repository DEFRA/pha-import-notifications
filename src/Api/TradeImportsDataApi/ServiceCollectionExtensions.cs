using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.TradeImportsDataApi;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddTradeImportsDataApiHttpClient(this IServiceCollection services)
    {
        return services
            .AddHttpClient<TradeDataHttpClient, TradeDataHttpClient>(
                (sp, httpClient) =>
                {
                    var options = sp.GetRequiredService<IOptions<TradeImportsDataApiOptions>>().Value;

                    httpClient.BaseAddress = new Uri(options.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );

                    if (options.Username is not null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"))
                        );
                    }

                    if (httpClient.BaseAddress.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                        httpClient.DefaultRequestVersion = HttpVersion.Version20;
                }
            )
            .AddHeaderPropagation();
    }
}
