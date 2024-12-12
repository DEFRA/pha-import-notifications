using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.BtmsStub;
using Defra.PhaImportNotifications.Testing;
using Microsoft.Extensions.Logging.Abstractions;
using ChedReferenceNumbers = Defra.PhaImportNotifications.Testing.ChedReferenceNumbers;

namespace Defra.PhaImportNotifications.Api.Tests.Services.Btms;

public class BtmsServiceTests(WireMockContext context) : WireMockTestBase(context)
{
    private BtmsService Subject { get; } =
        new(new JsonApiClient(context.HttpClient, NullLogger<JsonApiClient>.Instance));

    [Fact]
    public async Task GetImportNotificationUpdates_WhenOk_ShouldSucceed()
    {
        var bcp = new[] { "bcp1", "bcp2" };
        WireMock.StubImportNotificationUpdates(
            filter: "and(any(_PointOfEntry,'bcp1','bcp2'),any(importNotificationType,'Cveda','Cvedp','Chedpp','Ced'),not(equals(status,'Draft')))",
            fields: ["fields[import-notifications]=updated,referenceNumber"],
            transformRequest: builder => builder.WithJsonApiParam("page[size]=1000")
        );

        var result = await Subject.GetImportNotificationUpdates(bcp, CancellationToken.None);

        // If this fails, check the expected filter or fields as it may have changed
        result.Should().HaveCount(10);

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotificationUpdates_WhenOk_MultiplePages_ShouldSucceed()
    {
        var bcp = new[] { "bcp1", "bcp2" };

        // First request maps to path /api/import-notifications and returns next
        // link for second page
        WireMock.StubImportNotificationUpdates(transformBody: jsonNode =>
        {
            jsonNode["links"]!["next"] = "/api/import-notifications?page=2";
            return jsonNode;
        });
        // Second page request maps to path /api/import-notifications with param page=2
        // which then includes no next page link by default
        WireMock.StubImportNotificationUpdates(
            path: "/api/import-notifications",
            transformRequest: builder => builder.WithParam("page", "2")
        );

        var result = await Subject.GetImportNotificationUpdates(bcp, CancellationToken.None);

        result.Should().HaveCount(20);
    }

    [Fact]
    public async Task GetImportNotificationUpdates_WhenError_ShouldFail()
    {
        WireMock.StubImportNotificationUpdates(shouldFail: true);

        var act = () => Subject.GetImportNotificationUpdates([], CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetImportNotification_WhenOk_ShouldSucceed()
    {
        WireMock.StubSingleImportNotification();

        var result = await Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

        result.Should().NotBeNull();

        await Verify(result).DontScrubGuids().DontScrubDateTimes();
    }

    [Fact]
    public async Task GetImportNotification_WhenError_ShouldFail()
    {
        WireMock.StubSingleImportNotification(shouldFail: true);

        var act = () => Subject.GetImportNotification(ChedReferenceNumbers.ChedA, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }
}
