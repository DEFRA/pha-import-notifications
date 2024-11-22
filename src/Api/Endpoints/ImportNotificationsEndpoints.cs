using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ImportNotificationsEndpoints
{
    public static void MapImportNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{referenceNumber}/", Get)
            .WithName("ImportNotificationsByReferenceNumber")
            .WithTags("Import Notifications")
            .WithSummary("Get Import Notification")
            .WithDescription("Get an Import Notification by reference number")
            .Produces<ImportNotificationResponse>();
    }

    [HttpGet]
    private static async Task<IResult> Get(
        [FromRoute] [Description("Reference number")] string referenceNumber,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notification = await btmsService.GetImportNotification(referenceNumber, cancellationToken);

        return notification is not null ? Results.Ok(notification) : Results.NotFound();
    }

    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    private sealed class ImportNotificationResponse : ImportNotification;
}
