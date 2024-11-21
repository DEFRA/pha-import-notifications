using System.ComponentModel;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts.UpdatedImportNotifications;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ImportNotificationUpdatesEndpoint
{
    public static void MapImportNotificationUpdatesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications-updates/{portHealthAuthority}/", GetAllUpdated)
            .WithName("ImportNotificationsUpdatesByReferenceNumber")
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
        HttpContext httpContext,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        await btmsService.GetImportNotifications(cancellationToken);

        return Results.Ok(
            new PagedResponse<UpdatedImportNotification>
            {
                Records =
                [
                    new()
                    {
                        Links = new()
                        {
                            ImportNotification = new Uri(
                                $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/import-notifications/CHED1234/"
                            ),
                        },
                    },
                ],
                CurrentPage = 0,
                TotalPages = 1,
                TotalRecords = 1,
            }
        );
    }

    public class PagedResponse<T>
    {
        public List<T> Records { get; set; } = new();
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
