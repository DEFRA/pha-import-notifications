using System.Net;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints.ImportNotifications;

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
        var client = CreateClient(ClientId.WithFullAccess);

        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath(TradeImportsDataApiHttpClient.Endpoints.ImportNotification(ChedReferenceNumbers.ChedA))
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

        var response = await client.GetAsync($"/import-notifications/{ChedReferenceNumbers.ChedA}");

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        await VerifyJson(await response.Content.ReadAsStringAsync(), _verifySettings).ScrubMember("traceId");
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "TradeImportsDataApi:BaseUrl", HttpClient.BaseAddress?.ToString() } }
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
