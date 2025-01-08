using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using Microsoft.Extensions.Configuration;
using WireMock.Server;
using Xunit.Abstractions;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class GetTestsCompareSoap : EndpointTestBase, IClassFixture<WireMockContext>
{
    public GetTestsCompareSoap(
        ApiWebApplicationFactory factory,
        ITestOutputHelper outputHelper,
        WireMockContext context
    )
        : base(factory, outputHelper)
    {
        WireMock = context.Server;
        WireMock.Reset();
        HttpClient = context.HttpClient;
    }

    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    [Fact]
    public async Task Get_ChedAFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedAFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedDFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedDFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedPFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedPFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ChedPPFromSoap()
    {
        var response = await StubAndCall(ChedReferenceNumbers.ChedPPFromSoap);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    private async Task<string> StubAndCall(string chedReferenceNumber)
    {
        var client = CreateClient();

        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        return await client.GetStringAsync(Testing.Endpoints.ImportNotifications.Get(chedReferenceNumber));
    }

    protected override void ConfigureHostConfiguration(IConfigurationBuilder config)
    {
        config.AddInMemoryCollection(
            new Dictionary<string, string?> { { "Btms:BaseUrl", HttpClient.BaseAddress?.ToString() } }
        );

        base.ConfigureHostConfiguration(config);
    }
}
