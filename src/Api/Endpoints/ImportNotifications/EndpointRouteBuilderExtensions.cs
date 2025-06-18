using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Defra.PhaImportNotifications.Api.Endpoints.Validation;
using Defra.PhaImportNotifications.Api.Extensions;
using Defra.PhaImportNotifications.Api.Services;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

public static class EndpointRouteBuilderExtensions
{
    private static readonly string[] s_importNotificationTypes =
    [
        .. typeof(ImportPreNotification)
            .GetProperty(nameof(ImportPreNotification.ImportNotificationType))!
            .GetCustomAttributes<ExampleValueAttribute>()
            .Select(v => v.Value),
    ];

    public static void MapImportNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications", GetUpdated)
            .RequireAuthorization()
            .WithName("UpdatedImportNotifications")
            .WithTags("Import Notifications")
            .WithSummary("Get updated Import Notifications")
            .WithDescription("Get all Import Notifications that have been updated between the time period specified")
            .Produces<UpdatedImportNotificationsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<UpdatedImportNotificationRequestValidator>();

        app.MapGet("import-notifications/{chedReferenceNumber}/", Get)
            .RequireAuthorization()
            .WithName("ImportNotificationsByChedReferenceNumber")
            .WithTags("Import Notifications")
            .WithSummary("Get Import Notification")
            .WithDescription("Get an Import Notification by CHED Reference Number")
            .Produces<ImportNotificationResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status429TooManyRequests)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }

    /// <param name="request">Request</param>
    /// <param name="tradeImportsDataApiService">Trade Imports Data API Service</param>
    /// <param name="httpContext">HTTP Context</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpGet]
    private static async Task<IResult> GetUpdated(
        [AsParameters] UpdatedImportNotificationRequest request,
        [FromServices] ITradeImportsDataApiService tradeImportsDataApiService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    )
    {
        var bcps = request.Bcp ?? [];
        if (!httpContext.User.ClientHasAccessTo(bcps.ToList()))
            return Results.Forbid();

        var notifications = await tradeImportsDataApiService.GetImportNotificationUpdates(
            s_importNotificationTypes,
            bcps,
            request.From,
            request.To,
            request.Page,
            request.PageSize,
            cancellationToken
        );
        var updated = notifications.Select(x => new UpdatedImportNotification
        {
            Updated = x.UpdatedEntity,
            ReferenceNumber = x.ReferenceNumber,
            Links = new UpdatedImportNotificationLinks
            {
                ImportNotification = new Uri($"/import-notifications/{x.ReferenceNumber}"),
            },
        });

        return Results.Ok(
            new UpdatedImportNotificationsResponse
            {
                ImportNotifications = updated,
                Paging = new PagingMetadata
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    Total = updated.Count(),
                },
            }
        );
    }

    /// <param name="chedReferenceNumber" example="CHEDA.GB.2024.1020304">CHED Reference Number</param>
    /// <param name="tradeImportsDataApiService">Trade Imports Data API Service</param>
    /// <param name="httpContext">HTTP Context</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    [HttpGet]
    private static async Task<IResult> Get(
        [FromRoute]
        [Description("CHED Reference Number")]
        [RegularExpression($"^{Regexes.ChedReferenceNumber}$")]
            string chedReferenceNumber,
        [FromServices] ITradeImportsDataApiService tradeImportsDataApiService,
        HttpContext httpContext,
        CancellationToken cancellationToken
    )
    {
        var notification = await tradeImportsDataApiService.GetImportNotification(
            chedReferenceNumber,
            cancellationToken
        );
        var bcp = notification?.PartOne?.PointOfEntry;

        if (bcp is null)
            return Results.NotFound();

        return !httpContext.User.ClientHasAccessTo([bcp]) ? Results.NotFound() : Results.Ok(notification);
    }
}
