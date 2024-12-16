using System.Net;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WireMock.Server;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class GetTests : EndpointTestBase, IClassFixture<WireMockContext>
{
    public GetTests(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper, WireMockContext context)
        : base(factory, outputHelper)
    {
        WireMock = context.Server;
        WireMock.Reset();
        HttpClient = context.HttpClient;
    }

    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    [Fact]
    public async Task Get_WhenFound_ShouldSucceed()
    {
        var client = CreateClient();

        WireMock.StubSingleImportNotification();

        var response = await client.GetStringAsync(Testing.Endpoints.ImportNotifications.Get());

        // We mock BTMS with WireMock in order to test our APIs deserialisation
        // process. Rather than just mocking the service response itself. This
        // is so we can compare what is stubbed from BTMS against our verified
        // response below. If there are extended changes to what is stubbed
        // then it should be compared against our response below to ensure no
        // unexpected differences are introduced.

        // This test can also be used to compare what we are stubbing for BTMS
        // against what our API gives us. It can then be discussed.

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_WhenNotFound_ShouldNotBeFound()
    {
        var client = CreateClient();

        var response = await client.GetAsync(Testing.Endpoints.ImportNotifications.Get());

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Testing.Endpoints.ImportNotifications.Get());

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "Btms:BaseUrl", HttpClient.BaseAddress?.ToString() } }
        );

        base.ConfigureHostConfiguration(config);
    }
}
