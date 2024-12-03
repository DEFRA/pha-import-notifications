using System.Net;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Testing;
using Defra.PhaImportNotifications.Testing.Btms;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using WireMock.Server;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class ImportNotificationsEndpointsTests : EndpointTestBase<Program>, IClassFixture<WireMockContext>
{
    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    public ImportNotificationsEndpointsTests(WebApplicationFactory<Program> factory, WireMockContext context)
        : base(factory)
    {
        WireMock = context.Server;
        WireMock.Reset();
        HttpClient = context.HttpClient;
    }

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

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => new BtmsService(
            new JsonApiClient(HttpClient, NullLogger<JsonApiClient>.Instance)
        ));
    }
}
