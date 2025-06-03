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
    private ITradeImportsDataApiService MockTradeImportsDataApiService { get; } =
        Substitute.For<ITradeImportsDataApiService>();

    [Fact]
    public async Task Get_ShouldSucceed()
    {
        var client = CreateClient();
        var validRequest = new UpdatedImportNotificationRequest
        {
            Bcp = ["bcp1", "bcp2"],
            From = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc),
            To = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc),
        };

        SetUpMockTradeImportsDataApiServiceForSuccess(validRequest.Bcp, validRequest.From, validRequest.To);

        var url = Helpers.Endpoints.ImportNotifications.GetUpdatedValid(
            validRequest.Bcp,
            validRequest.From.ToString("O"),
            validRequest.To.ToString("O")
        );

        var response = await client.GetStringAsync(url);
        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
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
        var url = Helpers.Endpoints.ImportNotifications.GetUpdatedBetween(bcp, from, to);

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content).UseParameters(name).UseStrictJson().ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenRequestParamsAreInvalid_ToTooCloseToUtcNow_ShouldBeBadRequest()
    {
        var client = CreateClient();
        var url = Helpers.Endpoints.ImportNotifications.GetUpdatedBetween(
            ["bcp1"],
            DateTime.UtcNow.AddSeconds(-60).ToString("O"),
            DateTime.UtcNow.AddSeconds(-29).ToString("O")
        );

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content).UseStrictJson().ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndAuthorisedForAllBcps_ShouldSucceed()
    {
        var client = CreateClient("fsa");
        var from = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc);
        var to = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc);

        SetUpMockTradeImportsDataApiServiceForSuccess(bcps: [], from, to);

        var url = Helpers.Endpoints.ImportNotifications.GetUpdatedValid(from: from.ToString("O"), to: to.ToString("O"));
        var response = await client.GetStringAsync(url);

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_WhenBcpParameterIsNotProvided_AndNotAuthorisedForAllBcps_ReturnsForbidden()
    {
        var client = CreateClient("pha");
        var from = new DateTime(2024, 12, 12, 13, 10, 30, DateTimeKind.Utc);
        var to = new DateTime(2024, 12, 12, 13, 40, 30, DateTimeKind.Utc);

        var url = Helpers.Endpoints.ImportNotifications.GetUpdatedValid(from: from.ToString("O"), to: to.ToString("O"));
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Helpers.Endpoints.ImportNotifications.GetUpdatedValid());

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_WhenAuthenticatedButAccessToBcpDenied_ReturnsForbidden()
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            Helpers.Endpoints.ImportNotifications.GetUpdatedValid(["bcp1", "bcp2", "bcp-no-access"])
        );

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<ITradeImportsDataApiService>(_ => MockTradeImportsDataApiService);
    }

    private void SetUpMockTradeImportsDataApiServiceForSuccess(string[] bcps, DateTime from, DateTime to)
    {
        MockTradeImportsDataApiService
            .GetImportNotificationUpdates(
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
