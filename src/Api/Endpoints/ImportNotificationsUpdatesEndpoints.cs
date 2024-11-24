using System.ComponentModel;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts.UpdatedImportNotifications;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ImportNotificationsUpdatesEndpoints
{
    public static void MapImportNotificationsUpdatesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications-updates/{portHealthAuthority}/", Get)
            .WithName("ImportNotificationsUpdatesByReferenceNumber")
            .WithTags("Import Notifications Updates")
            .WithSummary("Get Import Notification Updates")
            .WithDescription(
                "Get all import notifications by port health authority that have been updated between the time period specified"
            )
            .Produces<PagedResponse<UpdatedImportNotification>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
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
        var links = notifications.Select(x => new UpdatedImportNotification
        {
            Links = new ImportNotificationLinks
            {
                ImportNotification = new Uri($"/import-notifications/{x.ReferenceNumber}"),
            },
        });

        return Results.Ok(
            new PagedResponse<UpdatedImportNotification>
            {
                Records = links,
                CurrentPage = 0,
                TotalPages = 1,
                TotalRecords = 1,
            }
        );
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed record UpdatedImportNotificationRequest
    {
        [Description("Allows a specific page to be requested")]
        [DefaultValue(1)]
        [FromQuery(Name = "page")]
        public int? Page { get; init; } = 1;

        [Description("Allows a page size to be requested")]
        [DefaultValue(100)]
        [FromQuery(Name = "pageSize")]
        public int? PageSize { get; init; } = 100;

        [Description("Filter import notifications updated after this date and time. Format is ISO 8601-1:2019")]
        [FromQuery(Name = "from")]
        public DateTime From { get; init; }

        [Description(
            "Filter import notifications updated before this date and time. Format is ISO 8601-1:2019. Default is now ie. "
                + "time of request. If the time period between from and to is greater than 24 hours then "
                + "the request will be invalid."
        )]
        [DefaultValue(typeof(DateTime), "Time of execution")] // This doesn't add anything to the spec - do we need it?
        [FromQuery(Name = "to")]
        public DateTime? To { get; init; } = DateTime.Now;
    }
}
