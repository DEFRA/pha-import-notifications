using System.Net;
using AutoFixture;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotificationsUpdates;

public class ImportNotificationsUpdatesEndpointsTests(
    TestWebApplicationFactory<Program> factory,
    ITestOutputHelper outputHelper
) : EndpointTestBase<Program>(factory, outputHelper)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact]
    public async Task Get_ShouldSucceed()
    {
        var client = CreateClient();
        var fixture = new Fixture();

        MockBtmsService
            .GetImportNotifications(Arg.Any<CancellationToken>())
            .Returns(
                new List<ImportNotification>
                {
                    fixture
                        .Build<ImportNotification>()
                        .With(x => x.ReferenceNumber, ChedReferenceNumbers.ChedA)
                        .With(x => x.Updated, new DateTime(2024, 11, 29, 23, 59, 59, DateTimeKind.Utc))
                        .Create(),
                }
            );

        var from = DateTime.Now.Subtract(TimeSpan.FromHours(1));
        var to = DateTime.Now.Subtract(TimeSpan.FromMinutes(30));

        var url = Testing.Endpoints.ImportNotificationsUpdates.Get(from, to);

        var response = await client.GetStringAsync(url);
        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ShouldFail_With400_BadRequest()
    {
        var client = CreateClient();

        var from = DateTime.Now.Subtract(TimeSpan.FromHours(4));
        var to = DateTime.Now.Subtract(TimeSpan.FromHours(1));

        var url = Testing.Endpoints.ImportNotificationsUpdates.Get(from, to);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => client.GetStringAsync(url));

        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        exception.Message.Should().Contain("Bad Request");
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
