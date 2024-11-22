using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests(WireMockContext context) : WireMockTestBase(context)
{
    private BtmsService Subject { get; } = new(context.HttpClient);

    [Fact]
    public async Task GetImportNotifications_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(
                Response.Create().WithStatusCode(StatusCodes.Status200OK).WithBodyAsJson(new { Data = new { } })
            );

        var result = await Subject.GetImportNotifications(default);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetImportNotifications_WhenOk_AndReturnsSingleItem_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("Services\\Btms\\btms-single-import-notification.json")
            );

        var result = await Subject.GetImportNotifications(default);

        result.Should().BeEmpty();
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
    public async Task GetImportNotification_Throws()
    {
        var act = () => Subject.GetImportNotification("CHED1234", default);

        await act.Should().ThrowAsync<NotImplementedException>();
    }
}
