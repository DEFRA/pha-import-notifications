using Microsoft.AspNetCore.Mvc;

namespace PhaImportNotifications.Endpoints;

public static class ImportNotificationUpdatesEndpoint
{
    public static void UseImportNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{referenceNumber}/", Get);
        app.MapGet("import-notifications-updates/{port}/", GetAll);
    }

    [HttpGet]
    [ProducesResponseType(typeof(object), 200)]
    private static Task<IResult> Get([FromRoute] string referenceNumber)
    {
        return Task.FromResult(Results.Ok());
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ImportNotificationUpdatesResponse), 200)]
    private static Task<IResult> GetAll(
        [FromRoute] string port,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        return Task.FromResult(Results.Ok());
    }
    
    public class ImportNotificationUpdatesResponse
    {
        public ImportNotificationUpdate[] Updates { get; set; }
    }
    
    public class ImportNotificationUpdate
    {
        public Update ImportNotification { get; set; }
        public Update Movements { get; set; }
        public Update GoodsMovements { get; set; }
        public ImportNotificationLinks Links { get; set; }
    }

    public class ImportNotificationLinks
    {
        public Uri Documents { get; set; }
        public Uri CustomsMovements { get; set; }
        public Uri GoodsMovements { get; set; }
        
    }
    
    public class Update
    {
        public DateTime Updated { get; set; }
        public Uri Path { get; set; }
    }
}