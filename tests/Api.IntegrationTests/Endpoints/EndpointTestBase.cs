using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class EndpointTestBase<T> : IClassFixture<TestWebApplicationFactory<T>>
    where T : class
{
    private readonly TestWebApplicationFactory<T> _factory;

    protected EndpointTestBase(TestWebApplicationFactory<T> factory, ITestOutputHelper outputHelper)
    {
        _factory = factory;
        _factory.OutputHelper = outputHelper;
    }

    protected virtual void ConfigureTestServices(IServiceCollection services) { }

    protected HttpClient CreateClient()
    {
        return _factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("IntegrationTests");
                builder.ConfigureTestServices(ConfigureTestServices);
            })
            .CreateClient();
    }
}
