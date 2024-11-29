using System.Net;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints.ImportNotifications;

public class ImportNotificationsEndpointsTests(WebApplicationFactory<Program> factory)
    : EndpointTestBase<Program>(factory)
{
    private IBtmsService MockBtmsService { get; } = Substitute.For<IBtmsService>();

    [Fact(Skip = "Not implemented yet")]
    public async Task Get_WhenFound_ShouldSucceed()
    {
        var client = CreateClient();

        MockBtmsService.GetImportNotification("mock1", Arg.Any<CancellationToken>()).ReturnsNull();

        var response = await client.GetStringAsync("import-notifications/mock1");

        await Verify(response);

        JsonConvert.DeserializeObject<ImportNotification>(response).Should().NotBeNull();
    }

    [Fact]
    public async Task Get_WhenNotFound_ShouldNotBeFound()
    {
        var client = CreateClient();

        var response = await client.GetAsync("import-notifications/mock1");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);
        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
