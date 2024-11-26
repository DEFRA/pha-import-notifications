using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ImportNotificationsEndpoints
{
    private const string ChedReferenceNumber = @"^CHED[A|D|P|PP]\.GB\.\d{4}\.\d{7}$";

    public static void MapImportNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{chedReferenceNumber}/", Get)
            .WithName("ImportNotificationsByChedReferenceNumber")
            .WithTags("Import Notifications")
            .WithSummary("Get Import Notification")
            .WithDescription("Get an Import Notification by CHED Reference Number")
            .Produces<ImportNotificationsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    /// <param name="chedReferenceNumber" example="CHEDA.GB.2024.1020304">CHED Reference Number</param>
    /// <param name="btmsService">BTMS Service</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpGet]
    private static async Task<IResult> Get(
        [FromRoute]
        [Description("CHED Reference Number")]
        [RegularExpression(ChedReferenceNumber)]
            string chedReferenceNumber,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notification = await btmsService.GetImportNotification(chedReferenceNumber, cancellationToken);

        return notification is not null ? Results.Ok(notification) : Results.NotFound();
    }

    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class ImportNotificationsResponse : ImportNotification;
}
