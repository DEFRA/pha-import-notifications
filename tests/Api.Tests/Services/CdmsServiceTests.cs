using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Defra.PhaImportNotifications.Api.Tests.Services;

public class CdmsServiceTests(WireMockContext context) : WireMockTestBase(context)
{
    private CdmsService Subject { get; } = new(context.HttpClient);

    [Fact]
    public async Task GetImportNotifications_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/api/import-notifications").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(StatusCodes.Status200OK));

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
}
