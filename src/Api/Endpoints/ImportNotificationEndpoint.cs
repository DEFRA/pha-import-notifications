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
    private static Task<IResult> Get([FromRoute] string referenceNumber)
    {
        return Task.FromResult(Results.Ok());
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get Import Notification Attachment")]
    [ProducesResponseType(typeof(ImportNotificationResponse), 200)]
    private static Task<IResult> GetAttachment([FromRoute] string referenceNumber, [FromRoute] string attachmentId)
    {
        return Task.FromResult(Results.Ok());
    }

    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    public class ImportNotificationResponse : ImportNotification { }
}
