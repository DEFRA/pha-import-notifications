using Defra.PhaImportNotifications.Api.JsonApi;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class ServiceCollectionExtensionsTests
{
    [Theory]
    [InlineData("http://localhost", 1, 1)]
    [InlineData("https://localhost", 2, 0)]
    public void AddJsonApiClient_AsExpected(string baseAddress, int expectedMajor, int expectedMinor)
    {
        var services = new ServiceCollection();

        services.AddJsonApiClient(_ => baseAddress);

        using var serviceProvider = services.BuildServiceProvider();

        serviceProvider.GetRequiredService<JsonApiClient>().Should().NotBeNull();

        var httpClient = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("JsonApiClient");

        httpClient.Should().NotBeNull();
        httpClient.BaseAddress.Should().Be(new Uri(baseAddress));
        httpClient.DefaultRequestHeaders.Accept.Should().ContainSingle(x => x.MediaType == "application/vnd.api+json");
        httpClient.DefaultRequestVersion.Should().Be(new Version(expectedMajor, expectedMinor));
    }
}
