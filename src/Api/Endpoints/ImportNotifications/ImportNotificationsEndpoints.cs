using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Defra.PhaImportNotifications.Api.Services.Btms;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public static class ImportNotificationsEndpoints
{
    public static void MapImportNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{chedReferenceNumber}/", Get)
            .AddEndpointFilter<ChedReferenceNumberValidation>()
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
        [RegularExpression($"^{Regexes.ChedReferenceNumber}$")]
            string chedReferenceNumber,
        [FromServices] IBtmsService btmsService,
        CancellationToken cancellationToken
    )
    {
        var notification = await btmsService.GetImportNotification(chedReferenceNumber, cancellationToken);

        return notification is not null ? Results.Ok(notification) : Results.NotFound();
    }

    private sealed class ChedReferenceNumberValidation : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next
        )
        {
            var chedReferenceNumber = context.GetArgument<string>(0);
            var validator = new ChedReferenceNumberValidator();
            var result = await validator.ValidateAsync(chedReferenceNumber, context.HttpContext.RequestAborted);

            if (!result.IsValid)
                return Results.ValidationProblem(result.ToDictionary());

            return await next.Invoke(context);
        }
    }
}
