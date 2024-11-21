using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.PhaImportNotifications.Api.IntegrationTests;

public class EndpointTestBase<T> : IClassFixture<WebApplicationFactory<T>>
    where T : class
{
    private readonly WebApplicationFactory<T> _factory;

    protected EndpointTestBase(WebApplicationFactory<T> factory)
    {
        _factory = factory;
    }

    protected virtual void ConfigureTestServices(IServiceCollection services) { }

    protected HttpClient CreateClient()
    {
        return _factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(ConfigureTestServices);
            })
            .CreateClient();
    }
}
