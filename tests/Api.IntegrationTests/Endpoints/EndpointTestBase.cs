using System.Net;
using System.Net.Sockets;
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

    private static int GetRandomPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();

        var port = ((IPEndPoint)listener.LocalEndpoint).Port;

        listener.Stop();
        return port;
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
        var btmsPort = GetRandomPort();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    { "Btms:BaseUrl", $"http://localhost:{btmsPort}" },
                    { "Btms:StubEnabled", "true" },
                    { "Btms:StubPort", btmsPort.ToString() },
                }
            )
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
