using System.Net;
using AutoFixture;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit.Abstractions;
using static Defra.PhaImportNotifications.Tests.Helpers.Endpoints;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints.ImportNotifications;

public class GetUpdatedTests(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper)
    : EndpointTestBase(factory, outputHelper)
{
    private readonly string[] _allImportNotificationTypes = ["CVEDA", "CVEDP", "CHEDPP", "CED"];

    private const int DEFAULT_PAGE = 1;

    private const int DEFAULT_PAGESIZE = 100;

    private ITradeImportsDataApiService MockTradeImportsDataApiService { get; } =
        Substitute.For<ITradeImportsDataApiService>();

    [Theory]
    [InlineData(new[] { "bcp1", "bcp2" }, new string[] { }, ClientId.WithLimitedBcpAccess, "With BCP Filter")]
    [InlineData(
        new[] { "bcp1", "bcp2" },
        new[] { "CVEDA", "CVEDP" },
        ClientId.WithFullAccess,
        "With BCP and Type Filter"
    )]
    [InlineData(new string[] { }, new[] { "CED", "CVEDP" }, ClientId.WithLimitedChedTypeAccess, "With Type Filter")]
    [InlineData(new string[] { }, new string[] { }, ClientId.WithFullAccess, "With no Filter")]
    public async Task Get_ShouldSucceed(string[] bcps, string[] chedType, string clientId, string scenarioName)
    {
        var client = CreateClient(clientId);
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = bcps,
            ChedType = chedType,
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(
            validRequest.ChedType.Any() ? validRequest.ChedType : _allImportNotificationTypes,
            validRequest.Bcp,
            validRequest.From,
            validRequest.To
        );

        var url = new UpdateUrlBuilder()
            .WithFrom(validRequest.From)
            .WithTo(validRequest.To)
            .WithBcp(validRequest.Bcp)
            .WithChedType(validRequest.ChedType)
            .Build();

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        await VerifyJson(content, _verifySettings).UseParameters(scenarioName);
    }

    [Theory]
    [InlineData("2024-12-11T10:00:00.000", "2024-12-11T10:30:00.000", new[] { "bcp1" }, "Utc")]
    [InlineData("2024-12-11T10:00:00.000Z", "2024-12-11T09:30:00.000Z", new[] { "bcp1" }, "FromBeforeTo")]
    [InlineData("2024-12-11T10:00:00.000Z", "2024-12-11T11:00:00.001Z", new[] { "bcp1" }, "FromToRange")]
    public async Task Get_WhenRequestParamsAreInvalid_ShouldBeBadRequest(
        string from,
        string to,
        string[] bcp,
        string name
    )
    {
        var client = CreateClient(ClientId.WithFullAccess);
        var url = new UpdateUrlBuilder().WithFrom(from).WithTo(to).WithBcp(bcp).Build();

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content, _verifySettings).UseParameters(name).ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenRequestParamsAreInvalid_ToTooCloseToUtcNow_ShouldBeBadRequest()
    {
        var client = CreateClient(ClientId.WithFullAccess);

        var now = DateTime.UtcNow;
        var url = new UpdateUrlBuilder().WithFrom(now.AddSeconds(-60)).WithTo(now.AddSeconds(-29)).Build();

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content, _verifySettings).ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndAuthorisedForAllBcps_ShouldSucceed()
    {
        var client = CreateClient("fsa");
        var from = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc);
        var to = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc);

        SetUpMockTradeImportsDataApiServiceForSuccess(importNotificationTypes: ["CVEDP", "CED"], bcps: [], from, to);

        var url = new UpdateUrlBuilder().WithChedType("CVEDP", "CED").WithPeriod(from: from, to: to).Build();

        var response = await client.GetStringAsync(url);

        await VerifyJson(response, _verifySettings);
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndNotAuthorisedForAllBcps_ReturnsForbidden()
    {
        var client = CreateClient(ClientId.WithLimitedBcpAccess);

        var url = new UpdateUrlBuilder().WithValidPeriod().WithChedType("CHEDA").Build();
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndNotAuthorisedForAllChedType_ReturnsForbidden()
    {
        var client = CreateClient(ClientId.WithLimitedChedTypeAccess);

        var url = new UpdateUrlBuilder().WithBcp("bcp-1").WithValidPeriod().Build();
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenPagingNotSpecified_DefaultsUsed()
    {
        var client = CreateClient();
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = [],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(
            _allImportNotificationTypes,
            validRequest.Bcp,
            validRequest.From,
            validRequest.To,
            DEFAULT_PAGE,
            DEFAULT_PAGESIZE
        );

        var url = new UpdateUrlBuilder().WithPeriod(validRequest.From, validRequest.To).Build();

        var response = await client.GetStringAsync(url);
        await VerifyJson(response, _verifySettings);
    }

    [Fact]
    public async Task Get_WhenPagingSpecified_ValuesPassedThrough()
    {
        var client = CreateClient();
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = [],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
            PageFromQuery = 3,
            PageSizeFromQuery = 12,
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(
            _allImportNotificationTypes,
            validRequest.Bcp,
            validRequest.From,
            validRequest.To,
            validRequest.Page,
            validRequest.PageSize
        );

        var url = new UpdateUrlBuilder()
            .WithPeriod(validRequest.From, validRequest.To)
            .WithPage(validRequest.Page)
            .WithPageSize(validRequest.PageSize)
            .Build();

        var response = await client.GetStringAsync(url);
        await VerifyJson(response, _verifySettings);
    }

    [Theory()]
    [InlineData(-1, HttpStatusCode.BadRequest)]
    [InlineData(0, HttpStatusCode.BadRequest)]
    [InlineData(1, HttpStatusCode.OK)]
    [InlineData(99, HttpStatusCode.OK)]
    [InlineData(999, HttpStatusCode.OK)]
    public async Task Get_WhenPageSpecified_LimitedToValidRange(int page, HttpStatusCode expectedStatusCode)
    {
        var client = CreateClient();
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = [],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
            PageFromQuery = page,
            PageSizeFromQuery = 25,
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(
            _allImportNotificationTypes,
            validRequest.Bcp,
            validRequest.From,
            validRequest.To,
            validRequest.Page,
            validRequest.PageSize
        );

        var url = new UpdateUrlBuilder()
            .WithPeriod(validRequest.From, validRequest.To)
            .WithPage(validRequest.Page)
            .WithPageSize(validRequest.PageSize)
            .Build();

        var response = await client.GetAsync(url);
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Theory()]
    [InlineData(-1, HttpStatusCode.BadRequest)]
    [InlineData(0, HttpStatusCode.BadRequest)]
    [InlineData(1, HttpStatusCode.OK)]
    [InlineData(999, HttpStatusCode.OK)]
    [InlineData(1000, HttpStatusCode.OK)]
    [InlineData(1001, HttpStatusCode.BadRequest)]
    [InlineData(99999, HttpStatusCode.BadRequest)]
    public async Task Get_WhenPageSizeSpecified_LimitedToValidRange(int pageSize, HttpStatusCode expectedStatusCode)
    {
        var client = CreateClient();
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = [],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
            PageFromQuery = 1,
            PageSizeFromQuery = pageSize,
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(
            _allImportNotificationTypes,
            validRequest.Bcp,
            validRequest.From,
            validRequest.To,
            validRequest.Page,
            validRequest.PageSize
        );

        var url = new UpdateUrlBuilder()
            .WithPeriod(validRequest.From, validRequest.To)
            .WithPage(validRequest.Page)
            .WithPageSize(validRequest.PageSize)
            .Build();

        var response = await client.GetAsync(url);
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient(ClientId.WithFullAccess);
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Helpers.Endpoints.UpdateUrlBuilder.GetUpdatedValid());

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToBcpDenied_ReturnsForbidden()
    {
        var client = CreateClient(ClientId.WithLimitedBcpAccess);

        var response = await client.GetAsync(
            new UpdateUrlBuilder().WithValidPeriod().WithBcp("bcp1", "bcp2", "bcp-no-access").Build()
        );
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToChedTypeDenied_ReturnsForbidden()
    {
        var client = CreateClient(ClientId.WithLimitedChedTypeAccess);

        var response = await client.GetAsync(new UpdateUrlBuilder().WithValidPeriod().WithChedType("CHEDPP").Build());

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient(_ => MockTradeImportsDataApiService);
    }

    private void SetUpMockTradeImportsDataApiServiceForSuccess(
        string[] importNotificationTypes,
        string[] bcps,
        DateTime from,
        DateTime to,
        int? page = null,
        int? pageSize = null
    )
    {
        MockTradeImportsDataApiService
            .GetImportNotificationUpdates(
                Arg.Is<string[]>(x => x.SequenceEqual(importNotificationTypes)),
                Arg.Is<string[]>(x => x.SequenceEqual(bcps)),
                from,
                to,
                page ?? Arg.Any<int>(),
                pageSize ?? Arg.Any<int>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(
                new Fixture()
                    .Build<ImportNotificationUpdatesPaged>()
                    .With(
                        x => x.ImportNotifications,
                        [
                            new Fixture()
                                .Build<ImportNotificationUpdate>()
                                .With(x => x.ReferenceNumber, ChedReferenceNumbers.ChedA)
                                .With(x => x.UpdatedEntity, new DateTime(2024, 11, 29, 23, 59, 59, DateTimeKind.Utc))
                                .Create(),
                        ]
                    )
                    .With(x => x.Page, page ?? 1)
                    .With(x => x.PageSize, pageSize ?? 100)
                    .With(x => x.Total, 1)
                    .Create()
            );
    }
}
