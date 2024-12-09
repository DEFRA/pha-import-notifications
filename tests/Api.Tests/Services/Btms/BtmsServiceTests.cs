using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests(WireMockContext context) : WireMockTestBase(context)
{
    private BtmsService Subject { get; } =
        new(new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance));

    [Fact]
    public async Task GetImportNotifications_WhenOk_ShouldSucceed()
    {
        var bcp = new[] { "bcp1", "bcp2" };
        WireMock.StubManyImportNotification(filter: "any(_PointOfEntry,'bcp1','bcp2')");

        var result = await Subject.GetImportNotifications(bcp, default);

        result.Should().HaveCount(10);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotifications_WhenError_ShouldFail()
    {
        WireMock.StubManyImportNotification(shouldFail: true);

        var act = () => Subject.GetImportNotifications([], default);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetImportNotification_WhenOk_ShouldSucceed()
    {
        WireMock.StubSingleImportNotification();

        var result = await Subject.GetImportNotification(ChedReferenceNumbers.ChedA, default);

        result.Should().NotBeNull();

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotification_WhenError_ShouldFail()
    {
        WireMock.StubSingleImportNotification(shouldFail: true);

        var act = () => Subject.GetImportNotification(ChedReferenceNumbers.ChedA, default);

        await act.Should().ThrowAsync<Exception>();
    }
}
