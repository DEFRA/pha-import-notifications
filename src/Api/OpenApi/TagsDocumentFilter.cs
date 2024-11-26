using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Defra.PhaImportNotifications.Api.OpenApi;

public class TagsDocumentFilter : IDocumentFilter
{
    private const string ImportNotificationsTag = "Import Notifications";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Extensions["tags"] = new OpenApiArray
        {
            new OpenApiObject
            {
                ["name"] = new OpenApiString(ImportNotificationsTag),
                ["description"] = new OpenApiString("Get updated import notifications for a PHA"),
            },
        };

        swaggerDoc.Extensions["x-tagGroups"] = new OpenApiArray
        {
            new OpenApiObject
            {
                ["name"] = new OpenApiString("Endpoints"),
                ["tags"] = new OpenApiArray { new OpenApiString(ImportNotificationsTag) },
            },
        };
    }
}
