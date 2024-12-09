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
            .GetImportNotifications(Arg.Is<string[]>(x => x.SequenceEqual(bcp)), Arg.Any<CancellationToken>())
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

        var url = Testing.Endpoints.ImportNotifications.GetUpdated(from, to, bcp);

        var response = await client.GetStringAsync(url);
        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task Get_ShouldFail_With400_BadRequest()
    {
        var client = CreateClient();
        var bcp = new[] { "bcp1", "bcp2" };

        var from = DateTime.Now.Subtract(TimeSpan.FromHours(4));
        var to = DateTime.Now.Subtract(TimeSpan.FromHours(1));

        var url = Testing.Endpoints.ImportNotifications.GetUpdated(from, to, bcp);

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
