using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests(WireMockContext context) : WireMockTestBase(context)
{
    private BtmsService Subject { get; } =
        new(new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance));

    [Fact]
    public async Task GetImportNotifications_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("Services\\Btms\\btms-import-notification-list.json")
            );

        var result = await Subject.GetImportNotifications(default);

        result.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetImportNotifications_WhenError_ShouldFail()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(StatusCodes.Status500InternalServerError));

        var act = () => Subject.GetImportNotifications(default);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetImportNotification_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications/CHED").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("Services\\Btms\\btms-import-notification-single.json")
            );

        var result = await Subject.GetImportNotification("CHED", default);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetImportNotification_WhenError_ShouldFail()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications/CHED").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(StatusCodes.Status500InternalServerError));

        var act = () => Subject.GetImportNotification("CHED", default);

        await act.Should().ThrowAsync<Exception>();
    }
}
