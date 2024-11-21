using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotificationsUpdates;

public class ImportNotificationsUpdatesEndpointsTests(WebApplicationFactory<Program> factory)
    : EndpointTestBase<Program>(factory)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact]
    public async Task GetAllUpdated_ShouldSucceed()
    {
        var client = CreateClient();

        MockBtmsService
            .GetImportNotifications(Arg.Any<CancellationToken>())
            .Returns(new List<ImportNotification> { new() { ReferenceNumber = "mock1" } });

        var response = await client.GetStringAsync(
            "import-notifications-updates/pha?page=1&pageSize=1&from=2024-11-20&to=2024-11-20"
        );

        await Verify(response);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
