using System.Net;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class GetTestsWhenError : EndpointTestBase, IClassFixture<WireMockContext>
{
    public GetTestsWhenError(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper, WireMockContext context)
        : base(factory, outputHelper)
    {
        WireMock = context.Server;
        WireMock.Reset();
        HttpClient = context.HttpClient;
    }

    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    [Fact]
    public async Task Get_WhenError_ShouldFail()
    {
        var client = CreateClient();

        WireMock.StubSingleImportNotification(shouldFail: true);

        var response = await client.GetAsync(Testing.Endpoints.ImportNotifications.Get());

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        await VerifyJson(await response.Content.ReadAsStringAsync()).UseStrictJson().ScrubMember("traceId");
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "Btms:BaseUrl", HttpClient.BaseAddress?.ToString() } }
        );

        base.ConfigureHostConfiguration(config);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.ConfigureHttpClientDefaults(options =>
            options.AddStandardResilienceHandler(config =>
            {
                config.Retry.ShouldHandle = args => ValueTask.FromResult(false);
            })
        );
    }
}
