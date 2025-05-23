using System.Text.Encodings.Web;
using Argon;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Api.TradeImportsDataApi;
using Defra.PhaImportNotifications.Testing;
using Defra.PhaImportNotifications.Tests.BtmsStub;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests : WireMockTestBase<WireMockContextQueryParameterNoComma>
{
    private readonly VerifySettings _settings;

    public BtmsServiceTests(WireMockContextQueryParameterNoComma context)
        : base(context)
    {
        Subject = new BtmsService(
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
    private BtmsService Subject { get; }

    private UpdatedImportNotificationRequest ValidRequest { get; } =
        new()
        {
            Bcp = ["bcp1", "bcp2"],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
        };

    [Theory]
    [InlineData("noBcps")]
    [InlineData("FilterByBcp", "bcp1", "bcp2")]
    public async Task GetImportNotificationUpdates_WhenOk_ShouldSucceed(string testName, params string[] bcps)
    {
        WireMock.StubImportNotificationUpdates(transformRequest: builder =>
            builder
                .WithParam(
                    "filter",
                    "and("
                        + (bcps.Length == 0 ? "" : "any(_PointOfEntry,'bcp1','bcp2'),")
                        + "any(importNotificationType,'CVEDA','CVEDP','CHEDPP','CED'),"
                        + "not(equals(status,'Draft')),"
                        + "greaterOrEqual(updatedEntity,'2024-12-12T13:10:30.0000000Z'),"
                        + "lessThan(updatedEntity,'2024-12-12T13:40:30.0000000Z')"
                        + ")"
                )
                .WithParam(UrlEncoder.Default.Encode("fields[import-notifications]"), "updatedEntity,referenceNumber")
                .WithParam(UrlEncoder.Default.Encode("page[size]"), "100")
        );

        var result = await Subject.GetImportNotificationUpdates(
            bcps,
            ValidRequest.From,
            ValidRequest.To,
            CancellationToken.None
        );

        // If this fails, check the expected filter or fields as it may have changed
        result.Should().HaveCount(10);

        await Verify(result, _settings).UseParameters(testName);
    }

    [Fact]
    public async Task GetImportNotificationUpdates_MultiplePages_WhenOk_ShouldSucceed()
    {
        StubFirstUpdatesRequestAndAddSubsequentPage(2);
        StubSubsequentUpdatesRequestForPage(2);

        var result = await Subject.GetImportNotificationUpdates(
            ValidRequest.Bcp!,
            ValidRequest.From,
            ValidRequest.To,
            CancellationToken.None
        );

        result.Should().HaveCount(20);
    }

    [Fact]
    public async Task GetImportNotificationUpdates_MultiplePages_WhenPageReturnsNull_ShouldFail()
    {
        StubFirstUpdatesRequestAndAddSubsequentPage(2);
        StubSubsequentUpdatesRequestForPage(2, StatusCodes.Status404NotFound);

        var act = () =>
            Subject.GetImportNotificationUpdates(
                ValidRequest.Bcp!,
                ValidRequest.From,
                ValidRequest.To,
                CancellationToken.None
            );

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Next page was null");
    }

    [Fact]
    public async Task GetImportNotificationUpdates_WhenError_ShouldFail()
    {
        WireMock.StubImportNotificationUpdates(shouldFail: true);

        var act = () => Subject.GetImportNotificationUpdates([], DateTime.Now, DateTime.Now, CancellationToken.None);

        await act.Should().ThrowAsync<HttpRequestException>();
    }

    [Fact]
    public async Task GetImportNotificationUpdates_WhenNullDocument_ShouldFail()
    {
        var mockJsonApiClient = Substitute.For<IJsonApiClient>();
        var subject = new BtmsService(mockJsonApiClient, null!, Options);

        var act = () => subject.GetImportNotificationUpdates([], DateTime.Now, DateTime.Now, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Result was null");
    }

    /// <summary>
    /// First request maps to path /api/import-notifications and returns next
    /// link for second page.
    /// </summary>
    /// <param name="nextPage"></param>
    private void StubFirstUpdatesRequestAndAddSubsequentPage(int nextPage)
    {
        WireMock.StubImportNotificationUpdates(transformBody: jsonNode =>
        {
            jsonNode["links"]!["next"] = $"/api/import-notifications?page={nextPage}";
            return jsonNode;
        });
    }

    /// <summary>
    /// Second page request maps to path /api/import-notifications with param page=2
    /// which then includes no next page link by default.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="statusCode"></param>
    private void StubSubsequentUpdatesRequestForPage(int page, int? statusCode = null)
    {
        WireMock.StubImportNotificationUpdates(
            transformRequest: builder => builder.WithParam("page", page.ToString()),
            statusCode: statusCode
        );
    }
}
