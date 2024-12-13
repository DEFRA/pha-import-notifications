using System.Net;
using AutoFixture;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class GetUpdatedTests(TestWebApplicationFactory<Program> factory, ITestOutputHelper outputHelper)
    : EndpointTestBase<Program>(factory, outputHelper)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact]
    public async Task Get_ShouldSucceed()
    {
        var client = CreateClient();
        var fixture = new Fixture();
        var bcp = new[] { "bcp1", "bcp2" };

        MockBtmsService
            .GetImportNotificationUpdates(Arg.Is<string[]>(x => x.SequenceEqual(bcp)), Arg.Any<CancellationToken>())
            .Returns(
                new List<ImportNotificationUpdate>
                {
                    fixture
                        .Build<ImportNotificationUpdate>()
                        .With(x => x.ReferenceNumber, ChedReferenceNumbers.ChedA)
                        .With(x => x.Updated, new DateTime(2024, 11, 29, 23, 59, 59, DateTimeKind.Utc))
                        .Create(),
                }
            );

        var url = Testing.Endpoints.ImportNotifications.GetUpdatedValid(bcp: bcp);

        var response = await client.GetStringAsync(url);
        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Theory]
    [InlineData("2024-12-11T10:00:00.000", "2024-12-11T10:30:00.000", new[] { "bcp1" }, "Utc")]
    [InlineData("2024-12-11T10:00:00.000Z", "2024-12-11T09:30:00.000Z", new[] { "bcp1" }, "FromBeforeTo")]
    [InlineData("2024-12-11T10:00:00.000Z", "2024-12-11T11:00:00.001Z", new[] { "bcp1" }, "FromToRange")]
    [InlineData("2024-12-11T10:00:00.000Z", "2024-12-11T10:30:00.001Z", null, "NoBcp")]
    public async Task Get_WhenRequestParamsAreInvalid_ShouldBeBadRequest(
        string? from,
        string? to,
        string[]? bcp,
        string name
    )
    {
        var client = CreateClient();
        var url = Testing.Endpoints.ImportNotifications.GetUpdated(from, to, bcp);

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content).UseParameters(name).UseStrictJson().ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenRequestParamsAreInvalid_ToTooCloseToUtcNow_ShouldBeBadRequest()
    {
        var client = CreateClient();
        var url = Testing.Endpoints.ImportNotifications.GetUpdated(
            DateTime.UtcNow.AddSeconds(-60).ToString("O"),
            DateTime.UtcNow.AddSeconds(-29).ToString("O"),
            ["bcp1"]
        );

        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        await VerifyJson(content).UseStrictJson().ScrubMember("traceId");
    }

    [Fact]
    public async Task Get_WhenNotAuthenticated_ReturnsUnauthorized()
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization = null;

        var response = await client.GetAsync(Testing.Endpoints.ImportNotifications.GetUpdatedValid());

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
