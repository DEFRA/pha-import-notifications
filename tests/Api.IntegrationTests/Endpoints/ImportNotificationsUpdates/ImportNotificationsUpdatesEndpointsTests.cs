using Defra.PhaImportNotifications.Api.Endpoints;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotificationsUpdates;

public class ImportNotificationsUpdatesEndpointsTests(WebApplicationFactory<Program> factory)
    : EndpointTestBase<Program>(factory)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact]
    public async Task Get_ShouldSucceed()
    {
        var client = CreateClient();

        MockBtmsService.GetImportNotifications(Arg.Any<CancellationToken>()).Returns(new List<ImportNotification>());

        var response = await client.GetStringAsync("import-notifications-updates/pha?from=2024-11-20");

        await Verify(response);

        JsonConvert.DeserializeObject<PagedResponse<UpdatedImportNotification>>(response).Should().NotBeNull();
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
