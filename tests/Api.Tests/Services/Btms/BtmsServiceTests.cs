using Argon;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests : WireMockTestBase<WireMockContextQueryParameterNoComma>
{
    private readonly VerifySettings _settings;

    public BtmsServiceTests(WireMockContextQueryParameterNoComma context)
        : base(context)
    {
        Subject = new BtmsService(new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance), Options);

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

    [Fact]
    public async Task GetImportNotificationUpdates_WhenOk_ShouldSucceed()
    {
        WireMock.StubImportNotificationUpdates(transformRequest: builder =>
            builder
                .WithParam(
                    "filter",
                    "and("
                        + "any(_PointOfEntry,'bcp1','bcp2'),"
                        + "any(importNotificationType,'Cveda','Cvedp','Chedpp','Ced'),"
                        + "not(equals(status,'Draft')),"
                        + "greaterOrEqual(updatedEntity,'2024-12-12T13:10:30.0000000Z'),"
                        + "lessThan(updatedEntity,'2024-12-12T13:40:30.0000000Z')"
                        + ")"
                )
                .WithJsonApiParam("fields[import-notifications]", "updatedEntity,referenceNumber")
                .WithJsonApiParam("page[size]", "100")
        );

        var result = await Subject.GetImportNotificationUpdates(
            ValidRequest.Bcp,
            ValidRequest.From,
            ValidRequest.To,
            CancellationToken.None
        );

        // If this fails, check the expected filter or fields as it may have changed
        result.Should().HaveCount(10);

        await Verify(result, _settings);
    }

    [Fact]
    public async Task GetImportNotificationUpdates_MultiplePages_WhenOk_ShouldSucceed()
    {
        StubFirstUpdatesRequestAndAddSubsequentPage(2);
        StubSubsequentUpdatesRequestForPage(2);

        var result = await Subject.GetImportNotificationUpdates(
            ValidRequest.Bcp,
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
                ValidRequest.Bcp,
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
        var subject = new BtmsService(mockJsonApiClient, Options);

        var act = () => subject.GetImportNotificationUpdates([], DateTime.Now, DateTime.Now, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Result was null");
    }

    [Theory]
    [InlineData(ChedReferenceNumbers.ChedA)]
    // ChedReferenceNumbers.ChedD has a movement. See GetImportNotification_WithMovements_WhenOk_ShouldSucceed
    [InlineData(ChedReferenceNumbers.ChedP)]
    [InlineData(ChedReferenceNumbers.ChedPP)]
    public async Task GetImportNotification_WhenOk_ShouldSucceed(string chedReferenceNumber)
    {
        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        var result = await Subject.GetImportNotification(chedReferenceNumber, CancellationToken.None);

        result.Should().NotBeNull();

        await Verify(result, _settings).UseParameters(chedReferenceNumber);
    }

    [Fact]
    public async Task GetImportNotification_WhenError_ShouldFail()
    {
        WireMock.StubSingleImportNotification(shouldFail: true);

        var act = () => Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    public static TheoryData<string, string[]> ChedReferenceNumbersWithMovements = new()
    {
        {
            ChedReferenceNumbers.ChedPWithMovement,
            [MovementReferenceNumbers.Movement1, MovementReferenceNumbers.Movement2]
        },
        { ChedReferenceNumbers.ChedD, [MovementReferenceNumbers.Movement3] },
        { ChedReferenceNumbers.ChedPFinalised, [MovementReferenceNumbers.MovementFinalised] },
    };

    [Theory, MemberData(nameof(ChedReferenceNumbersWithMovements))]
    public async Task GetImportNotification_WithMovements_WhenOk_ShouldSucceed(
        string chedReferenceNumber,
        string[] movementReferenceNumbers
    )
    {
        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        foreach (var mrn in movementReferenceNumbers)
            WireMock.StubSingleMovement(mrn: mrn);

        var result = await Subject.GetImportNotification(chedReferenceNumber, CancellationToken.None);

        result.Should().NotBeNull();

        await Verify(result, _settings).UseParameters(chedReferenceNumber);
    }

    [Theory(Skip = "Stub GMR Data unavailable yet ")]
    [InlineData(ChedReferenceNumbers.ChedAWithGoodsMovement, new[] { GoodsMovementsReferences.GMRId1 })]
    public async Task GetImportNotification_WithGoodsMovements_WhenOk_ShouldSucceed(
        string chedReferenceNumber,
        string[] goodsMovementsReferences
    )
    {
        WireMock.StubSingleImportNotification(chedReferenceNumber: chedReferenceNumber);

        foreach (var gmrId in goodsMovementsReferences)
            WireMock.StubGmrs(gmrId: gmrId);

        var result = await Subject.GetImportNotification(chedReferenceNumber, CancellationToken.None);

        result.Should().NotBeNull();

        await Verify(result, _settings).UseParameters(chedReferenceNumber);
    }

    [Fact]
    public async Task GetImportNotification_WhenNotFound_ShouldBeNull()
    {
        var mockJsonApiClient = Substitute.For<IJsonApiClient>();
        var subject = new BtmsService(mockJsonApiClient, Options);

        var result = await subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetImportNotification_WhenDocumentDataIsNull_ShouldFail()
    {
        var mockJsonApiClient = Substitute.For<IJsonApiClient>();
        mockJsonApiClient
            .Get(Arg.Any<RequestUri>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Document?>(new Document()));
        var subject = new BtmsService(mockJsonApiClient, Options);

        var act = () => subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

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
            path: "/api/import-notifications",
            transformRequest: builder => builder.WithParam("page", page.ToString()),
            statusCode: statusCode
        );
    }
}
