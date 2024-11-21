using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
    private static Task<IResult> Get([FromRoute] [Description("Reference number")] string referenceNumber)
    {
        return Task.FromResult(Results.Ok(new ImportNotificationResponse()));
    }

    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    public class ImportNotificationResponse : ImportNotification;
}
