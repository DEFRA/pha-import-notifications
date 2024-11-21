using System.Net.Http.Json;
using Defra.PhaImportNotifications.Api.Endpoints;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts.UpdatedImportNotifications;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.Api.IntegrationTests.Endpoints;

public class ImportNotificationUpdatesEndpointTests : EndpointTestBase<Program>, IClassFixture<WireMockContext>
{
    private WireMockServer WireMock { get; }
    private HttpClient HttpClient { get; }

    public ImportNotificationUpdatesEndpointTests(WebApplicationFactory<Program> factory, WireMockContext context)
        : base(factory)
    {
        WireMock = context.Server;
        WireMock.Reset();

        HttpClient = context.HttpClient;
    }

    [Fact]
    public async Task GetAllUpdated_ShouldSucceed()
    {
        var client = CreateClient();

        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(StatusCodes.Status200OK));

        var result =
            await client.GetFromJsonAsync<ImportNotificationUpdatesEndpoint.PagedResponse<UpdatedImportNotification>>(
                "import-notifications-updates/pha?page=1&pageSize=1&from=2024-11-20&to=2024-11-20"
            );

        result.Should().NotBeNull();
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => new BtmsService(HttpClient));
    }
}
