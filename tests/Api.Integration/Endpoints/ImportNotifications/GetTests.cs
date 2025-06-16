using System.Net;
using Defra.PhaImportNotifications.Tests.Helpers;
using Defra.PhaImportNotifications.Tests.TradeImportsDataApiStub;
using Microsoft.Extensions.Configuration;
using WireMock.Server;
using Xunit.Abstractions;
using WireMockExtensions = Defra.PhaImportNotifications.Tests.TradeImportsDataApiStub.WireMockExtensions;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints.ImportNotifications;

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

        WireMock.StubImportNotificationAndSubPaths(ChedReferenceNumbers.ChedA);

        var response = await client.GetStringAsync(
            Helpers.Endpoints.ImportNotifications.Get(ChedReferenceNumbers.ChedA)
        );

        // We mock dependencies with WireMock in order to test our APIs deserialisation
        // process. Rather than just mocking the service response itself. This
        // is so we can compare what is stubbed from dependencies against our verified
        // response below. If there are extended changes to what is stubbed
        // then it should be compared against our response below to ensure no
        // unexpected differences are introduced.

        // This test can also be used to compare what we are stubbing
        // against what our API gives us. It can then be discussed.
        await VerifyJson(response, _verifySettings);
    }

    [Theory]
    [ClassData(typeof(AllStubChedReferenceNumbers))]
    public async Task Get_AllStubs_ShouldSucceed(string chedReferenceNumber)
    {
        var client = CreateClient("pha");

        WireMock.StubImportNotificationAndSubPaths(chedReferenceNumber: chedReferenceNumber);

        var response = await client.GetStringAsync(Helpers.Endpoints.ImportNotifications.Get(chedReferenceNumber));

        await VerifyJson(response, _verifySettings).UseParameters(chedReferenceNumber);
    }

    public class AllStubChedReferenceNumbers : TheoryData<string>
    {
        public AllStubChedReferenceNumbers()
        {
            foreach (var chedReferenceNumber in WireMockExtensions.GetAllStubChedReferenceNumbers())
                Add(chedReferenceNumber);
        }
    }

    [Fact]
    public async Task Get_WhenAuthorisedForAllBcps_ShouldSucceed()
    {
        var chedReferenceNumber = ChedReferenceNumbers.ChedD;
        var client = CreateClient("fsa");

        WireMock.StubImportNotificationAndSubPaths(chedReferenceNumber: chedReferenceNumber);

        var response = await client.GetAsync(Helpers.Endpoints.ImportNotifications.Get(chedReferenceNumber));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_WhenNotFound_ShouldNotBeFound()
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            Helpers.Endpoints.ImportNotifications.Get(ChedReferenceNumbers.ChedPWithMovement)
        );

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Helpers.Endpoints.ImportNotifications.Get(ChedReferenceNumbers.ChedA));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToBcpDenied_ReturnsForbidden()
    {
        var client = CreateClient();

        WireMock.StubImportNotificationAndSubPaths(
            ChedReferenceNumbers.ChedP,
            transformImportNotificationResponse: responseBody =>
            {
                responseBody["importPreNotification"]!["partOne"]!["pointOfEntry"] = "NOTALLOWED";
                return responseBody;
            }
        );

        var response = await client.GetAsync(Helpers.Endpoints.ImportNotifications.Get(ChedReferenceNumbers.ChedP));

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenCdpRequestId_ShouldPropagate()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Add("x-cdp-request-id", "REQUEST-ID");

        WireMock.StubImportNotificationAndSubPaths(
            ChedReferenceNumbers.ChedPFinalised,
            transformRequest: request => request.WithHeader("x-cdp-request-id", "REQUEST-ID")
        );

        var response = await client.GetAsync(
            Helpers.Endpoints.ImportNotifications.Get(ChedReferenceNumbers.ChedPFinalised)
        );

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "TradeImportsDataApi:BaseUrl", HttpClient.BaseAddress?.ToString() } }
        );

        base.ConfigureHostConfiguration(config);
    }
}
