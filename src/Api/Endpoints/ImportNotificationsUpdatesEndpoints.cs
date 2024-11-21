using System.ComponentModel;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts.UpdatedImportNotifications;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ImportNotificationsUpdatesEndpoints
{
    public static void MapImportNotificationsUpdatesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications-updates/{portHealthAuthority}/", GetAllUpdated)
            .WithName("ImportNotificationsUpdatesByReferenceNumber")
            .WithTags("Import Notifications Updates")
            .WithSummary("Get Import Notification Updates")
            .WithDescription("Get an Import Notification Updates by port health authority")
            .Produces<PagedResponse<UpdatedImportNotification>>();
    }

    [HttpGet]
    private static async Task<IResult> GetAllUpdated(
        [FromRoute] [Description("The port health authority with format XYZ")] string portHealthAuthority,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
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

    private sealed class PagedResponse<T>
    {
        public IEnumerable<T> Records { get; set; } = [];
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
