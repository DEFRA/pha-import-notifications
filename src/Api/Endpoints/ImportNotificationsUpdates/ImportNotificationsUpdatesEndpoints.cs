using System.ComponentModel;
using Defra.PhaImportNotifications.Api.Endpoints.Validation;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotificationsUpdates;

public static class ImportNotificationsUpdatesEndpoints
{
    public static void MapImportNotificationsUpdatesEndpoints(this IEndpointRouteBuilder app)
    {
        // Question: do we want versioning in our path at all?
        // e.g. we start with /v1/import-notifications-updates etc...
        // or v1 is implied and v2 can be added if ever needed
        app.MapGet("import-notifications-updates/{portHealthAuthority}/", Get)
            .WithName("ImportNotificationsUpdatesByPortHealthAuthority")
            .WithTags("Import Notifications")
            .WithSummary("Get Import Notification Updates")
            .WithDescription(
                "Get all import notifications by port health authority that have been updated between the time period specified"
            )
            .Produces<PagedResponse<UpdatedImportNotification>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<UpdatedImportNotificationRequestValidator>();

        // Will add things like RequireRateLimiting("policyname") and RequireAuthorization()
        // to the above to include additional information in the generated open API spec
    }

    [HttpGet]
    private static async Task<IResult> Get(
        [FromRoute] [Description("The port health authority with format TBC")] string portHealthAuthority,
        [AsParameters] UpdatedImportNotificationRequest request,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notifications = await btmsService.GetImportNotifications(cancellationToken);
        var records = notifications.Select(x => new UpdatedImportNotification
        {
            Updated = x.Updated,
            Uri = $"/import-notifications/{x.ReferenceNumber}",
        });

        return Results.Ok(
            new PagedResponse<UpdatedImportNotification>
            {
                Records = records,
                CurrentPage = 0,
                TotalPages = 1,
                TotalRecords = 1,
            }
        );
    }
}
