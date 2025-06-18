using System.Net;
using Argon;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Tests.Helpers;
using Defra.PhaImportNotifications.Tests.TradeImportsDataApiStub;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Tests.Helpers.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Services;

public class TradeImportsDataApiServiceTests : WireMockTestBase<WireMockContextQueryParameterNoComma>
{
    private readonly VerifySettings _settings;

    public TradeImportsDataApiServiceTests(WireMockContextQueryParameterNoComma context)
        : base(context)
    {
        Subject = new TradeImportsDataApiService(
            new TradeImportsDataApiHttpClient(new HttpClient { BaseAddress = new Uri(context.Server.Urls[0]) })
        );
        _settings = new VerifySettings();
        _settings.DontScrubGuids();
        _settings.DontScrubDateTimes();
        _settings.AddExtraSettings(settings => settings.DefaultValueHandling = DefaultValueHandling.Include);
        _settings.UseDirectory("Verified");
    }

    private readonly string[] _importNotificationTypes = ["type1", "type2", "type3"];

    private UpdatedImportNotificationRequest ValidRequest { get; } =
        new()
        {
            Bcp = ["bcp1", "bcp2"],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
            PageFromQuery = 5,
            PageSizeFromQuery = 17,
        };

    private TradeImportsDataApiService Subject { get; }

    [Theory]
    [InlineData("noBcps")]
    [InlineData("FilterByBcp", "bcp1", "bcp2")]
    public async Task GetImportNotificationUpdates_WhenOk_ShouldSucceed(string testName, params string[] bcps)
    {
        WireMock.StubImportNotificationUpdates(transformRequest: builder =>
        {
            builder
                .WithParam("from", "2024-12-12T13:10:30.0000000Z")
                .WithParam("to", "2024-12-12T13:40:30.0000000Z")
                .WithParam("excludeStatus", "DRAFT")
                .WithParam("type", "type1", "type2", "type3")
                .WithParam("page", "5")
                .WithParam("pageSize", "17");
            if (bcps.Length > 0)
            {
                builder.WithParam("pointOfEntry", "bcp1", "bcp2");
            }
            return builder;
        });

        var result = await Subject.GetImportNotificationUpdates(
            _importNotificationTypes,
            bcps,
            ValidRequest.From,
            ValidRequest.To,
            ValidRequest.Page,
            ValidRequest.PageSize,
            CancellationToken.None
        );

        // If this fails, check the expected filter or fields as it may have changed
        result.Should().HaveCount(10);

        await Verify(result, _settings).UseParameters(testName);
    }

    [Fact]
    public async Task GetImportNotificationUpdates_WhenError_ShouldFail()
    {
        WireMock.StubImportNotificationUpdates(shouldFail: true);

        var act = () =>
            Subject.GetImportNotificationUpdates(
                _importNotificationTypes,
                [],
                DateTime.Now,
                DateTime.Now,
                1,
                10,
                CancellationToken.None
            );

        await act.Should().ThrowAsync<HttpRequestException>();
    }

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
                    .WithPath(TradeImportsDataApiHttpClient.Endpoints.ImportNotification(ChedReferenceNumbers.ChedA))
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
                    .WithPath(TradeImportsDataApiHttpClient.Endpoints.ImportNotification(ChedReferenceNumbers.ChedA))
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.NotFound));

        var result = await Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);
        result.Should().BeNull();
    }
}
