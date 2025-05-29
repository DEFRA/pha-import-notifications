using System.Net;
using Argon;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Api.TradeImportsDataApi;
using Defra.PhaImportNotifications.Tests.BtmsStub;
using Defra.PhaImportNotifications.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Tests.Helpers.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Services;

public class TradeDataApiServiceTests : WireMockTestBase<WireMockContextQueryParameterNoComma>
{
    private readonly VerifySettings _settings;

    public TradeDataApiServiceTests(WireMockContextQueryParameterNoComma context)
        : base(context)
    {
        Subject = new TradeDataImportsService(
            new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance),
            new TradeDataHttpClient(new HttpClient { BaseAddress = new Uri(context.Server.Urls[0]) }),
            Options
        );
        _settings = new VerifySettings();
        _settings.DontScrubGuids();
        _settings.DontScrubDateTimes();
        _settings.AddExtraSettings(settings => settings.DefaultValueHandling = DefaultValueHandling.Include);
    }

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
    private TradeDataImportsService Subject { get; }

    [Theory]
    [InlineData(ChedReferenceNumbers.ChedA)]
    // ChedReferenceNumbers.ChedD has a movement. See GetImportNotification_WithMovements_WhenOk_ShouldSucceed
    [InlineData(ChedReferenceNumbers.ChedP)]
    [InlineData(ChedReferenceNumbers.ChedPP)]
    [InlineData(ChedReferenceNumbers.ChedPWithMovement)]
    [InlineData(ChedReferenceNumbers.ChedPFinalised)]
    [InlineData(ChedReferenceNumbers.ChedAWithGoodsMovement)]
    public async Task GetImportNotification_WhenOk_ShouldSucceed(string chedReferenceNumber)
    {
        WireMock.StubImportNotificationAndSubPaths(chedReferenceNumber: chedReferenceNumber);

        var result = await Subject.GetImportNotification(chedReferenceNumber, CancellationToken.None);

        result.Should().NotBeNull();
        await Verify(result, _settings).UseParameters(chedReferenceNumber);
    }

    [Fact]
    public async Task GetImportNotification_WhenError_ShouldFail()
    {
        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath(TradeDataHttpClient.Endpoints.ImportNotification(ChedReferenceNumbers.ChedA))
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

        var act = () => Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetImportNotification_WhenNotFound_ShouldBeNull()
    {
        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath(TradeDataHttpClient.Endpoints.ImportNotification(ChedReferenceNumbers.ChedA))
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.NotFound));

        var result = await Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);
        result.Should().BeNull();
    }
}
