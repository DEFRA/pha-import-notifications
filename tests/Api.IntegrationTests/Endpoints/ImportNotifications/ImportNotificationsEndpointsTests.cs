using System.Net;
using AutoFixture;
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

    [Fact]
    public async Task Get_WhenFound_ShouldSucceed()
    {
        var client = CreateClient();
        var fixture = new Fixture();

        MockBtmsService
            .GetImportNotification("CHEDA.GB.2024.1234567", Arg.Any<CancellationToken>())
            .Returns(fixture.Create<ImportNotification>());

        var response = await client.GetStringAsync("import-notifications/CHEDA.GB.2024.1234567");

        // When we integrate, we will replace this with a Verify call working
        // against a builder for ImportNotification that will be generating
        // known scenarios

        JsonConvert.DeserializeObject<ImportNotification>(response).Should().NotBeNull();
    }

    [Fact]
    public async Task Get_WhenNotFound_ShouldNotBeFound()
    {
        var client = CreateClient();

        var response = await client.GetAsync("import-notifications/CHEDA.GB.2024.1234567");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_WhenInvalidChedReferenceNumber_ShouldBeBadRequest()
    {
        var client = CreateClient();

        var response = await client.GetAsync("import-notifications/12345");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadAsStringAsync()).Should().Contain("ChedReferenceNumber");
    }

    protected override void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureTestServices(services);

        services.AddTransient<IBtmsService>(_ => MockBtmsService);
    }
}
