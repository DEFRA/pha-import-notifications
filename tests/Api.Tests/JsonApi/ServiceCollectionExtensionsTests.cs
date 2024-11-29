using Defra.PhaImportNotifications.Api.JsonApi;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddJsonApiClient_AsExpected()
    {
        var services = new ServiceCollection();

        services.AddJsonApiClient(sp => "http://localhost");

        using var serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetRequiredService<JsonApiClient>().Should().NotBeNull();

        var httpClient = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("JsonApiClient");

        httpClient.Should().NotBeNull();
        httpClient.BaseAddress.Should().Be(new Uri("http://localhost"));
        httpClient.DefaultRequestHeaders.Accept.Should().ContainSingle(x => x.MediaType == "application/vnd.api+json");
    }
}
