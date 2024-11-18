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
        [FromQuery] int limit,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to
    )
    {
        return Task.FromResult(
            Results.Ok(
                new PagedResponse<UpdatedImportNotification>()
                {
                    CurrentPage = 0,
                    TotalPages = 0,
                    TotalRecords = 0,
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
