using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public static class EndpointRouteBuilderExtensions
{
    public static void MapImportNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications", GetUpdated)
            .WithName("UpdatedImportNotifications")
            .WithTags("Import Notifications")
            .WithSummary("Get updated Import Notifications")
            .WithDescription("Get all Import Notifications that have been updated between the time period specified")
            .Produces<UpdatedImportNotificationsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        app.MapGet("import-notifications/{chedReferenceNumber}/", Get)
            .WithName("ImportNotificationsByChedReferenceNumber")
            .WithTags("Import Notifications")
            .WithSummary("Get Import Notification")
            .WithDescription("Get an Import Notification by CHED Reference Number")
            .Produces<ImportNotificationResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    /// <param name="request">Request</param>
    /// <param name="btmsService">BTMS Service</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpGet]
    private static async Task<IResult> GetUpdated(
        [AsParameters] UpdatedImportNotificationRequest request,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notifications = await btmsService.GetImportNotifications(cancellationToken);
        var updated = notifications.Select(x => new UpdatedImportNotification
        {
            Updated = x.Updated,
            ReferenceNumber = x.ReferenceNumber!,
            Links = new UpdatedImportNotificationLinks
            {
                ImportNotification = new Uri($"/import-notifications/{x.ReferenceNumber}"),
            },
        });

        return Results.Ok(new UpdatedImportNotificationsResponse { ImportNotifications = updated });
    }

    /// <param name="chedReferenceNumber" example="CHEDA.GB.2024.1020304">CHED Reference Number</param>
    /// <param name="btmsService">BTMS Service</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpGet]
    private static async Task<IResult> Get(
        [FromRoute]
        [Description("CHED Reference Number")]
        [RegularExpression($"^{Regexes.ChedReferenceNumber}$")]
            string chedReferenceNumber,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notification = await btmsService.GetImportNotification(chedReferenceNumber, cancellationToken);

        return notification is not null ? Results.Ok(notification) : Results.NotFound();
    }
}
