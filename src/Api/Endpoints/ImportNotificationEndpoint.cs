using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trade.ImportNotification.Contract;
using Trade.ImportNotification.Contract.CustomsMovements;
using Trade.ImportNotification.Contract.GoodsMovements;
using Trade.ImportNotification.Contract.UpdatedImportNotifications;

namespace Api.Endpoints;

public static class ImportNotificationEndpoint
{
    public static void UseImportNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("import-notifications/{referenceNumber}/", Get);
        app.MapGet("import-notifications/{referenceNumber}/goods-movements", GetImportNotificationCustomsMovements);
        app.MapGet("import-notifications/{referenceNumber}/customs-movements", GetImportNotificationCustomsMovements);
        app.MapGet("import-notifications/{referenceNumber}/attachment/{attachmentId}", GetAttachment);
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
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get Import Notification Customs Movements")]
    [ProducesResponseType(typeof(CustomsMovementsResponse), 200)]
    private static Task<IResult> GetImportNotificationCustomsMovements([FromRoute] string referenceNumber)
    {
        return Task.FromResult(Results.Ok());
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get Import Notification Goods Movements")]
    [ProducesResponseType(typeof(GoodsMovementsResponse), 200)]
    private static Task<IResult> GetImportNotificationGoodsMovements([FromRoute] string referenceNumber)
    {
        return Task.FromResult(Results.Ok());
    }
    
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty")]
    public class ImportNotificationResponse : ImportNotification 
    {
        
    }
    
    public class GoodsMovementsResponse
    {
        public List<GoodsMovement> CustomsMovements { get; set; } = [];
    }
   
    public class CustomsMovementsResponse
    {
        public List<CustomsMovement> CustomsMovements { get; set; } = [];
    }
    
    
  
    
}