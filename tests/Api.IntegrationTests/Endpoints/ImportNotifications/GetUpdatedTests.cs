using AutoFixture;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Defra.PhaImportNotifications.Testing;
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

        var response = await client.GetStringAsync(Testing.Endpoints.ImportNotifications.GetUpdated());

        await VerifyJson(response).UseStrictJson().DontScrubGuids().DontScrubDateTimes();
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
