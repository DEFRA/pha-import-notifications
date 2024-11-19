using Microsoft.AspNetCore.Mvc;
using Trade.ImportNotification.Contract.UpdatedImportNotifications;

namespace Api.Endpoints;

public static class ImportNotificationUpdatesEndpoint
{
    public static void UseImportNotificationUpdatesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications-updates/{portHealthAuthority}/", GetAllUpdated);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<UpdatedImportNotification>), 200)]
    public static Task<IResult> GetAllUpdated(
        [FromRoute] string portHealthAuthority,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        HttpContext httpContext
    )
    {
        return Task.FromResult(
            Results.Ok(
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
            )
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
