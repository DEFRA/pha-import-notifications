using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class ImportNotificationsEndpointsTests(WebApplicationFactory<Program> factory)
    : EndpointTestBase<Program>(factory)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact]
    public async Task GetAllUpdated_ShouldSucceed()
    {
        var client = CreateClient();

        MockBtmsService
            .GetImportNotification("mock1", Arg.Any<CancellationToken>())
            .Returns(new ImportNotification { ReferenceNumber = "mock1" });

        var response = await client.GetStringAsync("import-notifications/mock1");

        await Verify(response);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
