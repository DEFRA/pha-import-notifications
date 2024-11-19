using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trade.ImportNotification.Contract;

namespace Api.Endpoints;

public static class ImportNotificationEndpoint
{
    public static void UseImportNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{referenceNumber}/", Get);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get Import Notification")]
    [ProducesResponseType(typeof(ImportNotificationResponse), 200)]
    public static Task<IResult> Get([FromRoute] string referenceNumber)
    {
        return Task.FromResult(Results.Ok(new ImportNotificationResponse()));
    }

    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    public class ImportNotificationResponse : ImportNotification;
}
