using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Testing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

/// <summary>
/// These tests are intended to generate response output from the API using stub data
/// from BTMS, which in turn was generated from the SOAP service CHED example files.
///
/// This allows us to perform a visual check to identify any data that the SOAP service
/// is returning but the API is not i.e. data missing from BTMS.
/// </summary>
/// <param name="context"></param>
public class BtmsServiceSoapTests(WireMockContextQueryParameterNoComma context)
    : WireMockTestBase<WireMockContextQueryParameterNoComma>(context)
{
    private static IOptions<BtmsOptions> Options { get; } =
        new OptionsWrapper<BtmsOptions>(
            new BtmsOptions
            {
                BaseUrl = "http://base-url",
                Password = "password",
                Username = "username",
                PageSize = 100,
            }
        );

    private BtmsService Subject { get; } =
        new(new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance), Options);

    [Fact]
    public async Task GetImportNotification_ChedAFromSoap()
    {
        var result = await StubAndCall(ChedReferenceNumbers.ChedAFromSoap);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotification_ChedDFromSoap()
    {
        var result = await StubAndCall(ChedReferenceNumbers.ChedDFromSoap);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotification_ChedPFromSoap()
    {
        var result = await StubAndCall(ChedReferenceNumbers.ChedPFromSoap);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotification_ChedPPFromSoap()
    {
        var result = await StubAndCall(ChedReferenceNumbers.ChedPPFromSoap);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    private async Task<ImportNotification?> StubAndCall(string chedReferenceNumber)
    {
        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        var result = await Subject.GetImportNotification(chedReferenceNumber, CancellationToken.None);

        result.Should().NotBeNull();

        return result;
    }
}
