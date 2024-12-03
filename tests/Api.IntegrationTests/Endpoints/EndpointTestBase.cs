using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class EndpointTestBase<T> : IClassFixture<WebApplicationFactory<T>>
    where T : class
{
    private readonly WebApplicationFactory<T> _factory;

    protected EndpointTestBase(WebApplicationFactory<T> factory)
    {
        _factory = factory;
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { { "Btms:StubEnabled", "false" } })
            .Build();

        services.Configure<BtmsOptions>(configuration.GetSection("Btms"));
    }

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
