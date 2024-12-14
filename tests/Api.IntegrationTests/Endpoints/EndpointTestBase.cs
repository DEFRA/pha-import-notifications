using System.Net.Http.Headers;
using Defra.PhaImportNotifications.Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class EndpointTestBase : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;

    protected EndpointTestBase(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper)
    {
        _factory = factory;
        _factory.OutputHelper = outputHelper;
    }

    protected virtual void ConfigureTestServices(IServiceCollection services) { }

    protected HttpClient CreateClient()
    {
        var builder = _factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("IntegrationTests");
            builder.ConfigureTestServices(ConfigureTestServices);
        });

        var client = builder.CreateClient();
        var config = builder.Services.GetService<IConfiguration>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            BasicAuthHelper.CreateBasicAuth(
                config?.GetValue<string>("BasicAuth:Username")!,
                config?.GetValue<string>("BasicAuth:Password")!
            )
        );

        return client;
    }
}
