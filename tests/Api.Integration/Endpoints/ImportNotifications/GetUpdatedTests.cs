using System.Net;
using AutoFixture;
using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Tests.Api.Integration.Endpoints.ImportNotifications;

public class GetUpdatedTests(ApiWebApplicationFactory factory, ITestOutputHelper outputHelper)
    : EndpointTestBase(factory, outputHelper)
{
    private readonly string[] _allImportNotificationTypes = ["CVEDA", "CVEDP", "CHEDPP", "CED"];

    private ITradeImportsDataApiService MockTradeImportsDataApiService { get; } =
        Substitute.For<ITradeImportsDataApiService>();

    private static class Clients
    {
        public const string ClientWithFullAccess = "defra";
        public const string ClientWithLimitedBcpAccess = "pha";
        public const string ClientWithLimitedChedTypeAccess = "fsa";
    }

    [Theory]
    [InlineData(new[] { "bcp1", "bcp2" }, new string[] { }, Clients.ClientWithLimitedBcpAccess, "With BCP Filter")]
    [InlineData(
        new[] { "bcp1", "bcp2" },
        new[] { "CVEDA", "CVEDP" },
        Clients.ClientWithFullAccess,
        "With BCP and Type Filter"
    )]
    [InlineData(
        new string[] { },
        new[] { "CED", "CVEDP" },
        Clients.ClientWithLimitedChedTypeAccess,
        "With Type Filter"
    )]
    [InlineData(new string[] { }, new string[] { }, Clients.ClientWithFullAccess, "With no Filter")]
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

        var url = new Helpers.Endpoints.UpdateUrlBuilder()
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
        var client = CreateClient();
        var url = new Helpers.Endpoints.UpdateUrlBuilder().WithFrom(from).WithTo(to).WithBcp(bcp).Build();

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content, _verifySettings).UseParameters(name).ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenRequestParamsAreInvalid_ToTooCloseToUtcNow_ShouldBeBadRequest()
    {
        var client = CreateClient();

        var now = DateTime.UtcNow;
        var url = new Helpers.Endpoints.UpdateUrlBuilder()
            .WithFrom(now.AddSeconds(-60))
            .WithTo(now.AddSeconds(-29))
            .Build();

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

        var url = new Helpers.Endpoints.UpdateUrlBuilder()
            .WithChedType("CVEDP", "CED")
            .WithPeriod(from: from, to: to)
            .Build();

        var response = await client.GetStringAsync(url);

        await VerifyJson(response, _verifySettings);
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndNotAuthorisedForAllBcps_ReturnsForbidden()
    {
        var client = CreateClient(Clients.ClientWithLimitedBcpAccess);

        var url = new Helpers.Endpoints.UpdateUrlBuilder().WithValidPeriod().WithChedType("CHEDA").Build();
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndNotAuthorisedForAllChedType_ReturnsForbidden()
    {
        var client = CreateClient(Clients.ClientWithLimitedChedTypeAccess);

        var url = new Helpers.Endpoints.UpdateUrlBuilder().WithBcp("bcp-1").WithValidPeriod().Build();
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Helpers.Endpoints.UpdateUrlBuilder.GetUpdatedValid());

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToBcpDenied_ReturnsForbidden()
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            new Helpers.Endpoints.UpdateUrlBuilder().WithValidPeriod().WithBcp("bcp1", "bcp2", "bcp-no-access").Build()
        );

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToChedTypeDenied_ReturnsForbidden()
    {
        var client = CreateClient(Clients.ClientWithLimitedChedTypeAccess);

        var response = await client.GetAsync(
            new Helpers.Endpoints.UpdateUrlBuilder().WithValidPeriod().WithChedType("CHEDPP").Build()
        );

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<ITradeImportsDataApiService>(_ => MockTradeImportsDataApiService);
    }

    private void SetUpMockTradeImportsDataApiServiceForSuccess(
        // ReSharper disable once UnusedParameter.Local
        string[] importNotificationTypes,
        string[] bcps,
        DateTime from,
        DateTime to
    )
    {
        MockTradeImportsDataApiService
            .GetImportNotificationUpdates(
                Arg.Is<string[]>(x => x.SequenceEqual(importNotificationTypes)),
                Arg.Is<string[]>(x => x.SequenceEqual(bcps)),
                from,
                to,
                Arg.Any<CancellationToken>()
            )
            .Returns(
                [
                    new Fixture()
                        .Build<ImportNotificationUpdate>()
                        .With(x => x.ReferenceNumber, ChedReferenceNumbers.ChedA)
                        .With(x => x.UpdatedEntity, new DateTime(2024, 11, 29, 23, 59, 59, DateTimeKind.Utc))
                        .Create(),
                ]
            );
    }
}
