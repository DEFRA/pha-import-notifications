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
            .ProducesProblem(StatusCodes.Status403Forbidden)
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
            .ProducesProblem(StatusCodes.Status403Forbidden)
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
        var chedTypes = request.ChedType?.Length > 0 ? request.ChedType : s_importNotificationTypes;

        var bcps = request.Bcp ?? [];
        if (!httpContext.User.ClientHasAccessTo(bcps.ToList(), chedTypes.ToList()))
            return Results.Forbid();

        var response = await tradeImportsDataApiService.GetImportNotificationUpdates(
            chedTypes,
            bcps,
            request.From,
            request.To,
            request.Page,
            request.PageSize,
            cancellationToken
        );
        var updated = response.ImportNotifications.Select(x => new UpdatedImportNotification
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
                    Page = response.Page,
                    PageSize = response.PageSize,
                    Total = response.Total,
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

        if (notification is null)
            return Results.NotFound();

        var bcp = notification.PartOne?.PointOfEntry;

        if (bcp is null)
            return Results.Forbid();

        return !httpContext.User.ClientHasAccessTo([bcp], [notification.ImportNotificationType!])
            ? Results.Forbid()
            : Results.Ok(notification);
    }
}
