using Microsoft.OpenApi;
using System.Text.Json.Nodes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Defra.PhaImportNotifications.Api.OpenApi;

public class TagsDocumentFilter : IDocumentFilter
{
    private const string ImportNotificationsTag = "Import Notifications";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var tag = swaggerDoc.Tags?.FirstOrDefault(t => t.Name == ImportNotificationsTag);
        if (tag is not null)
        {
            tag.Description = "Get updated import notifications for a PHA";
        }
        
        swaggerDoc.Extensions ??= new Dictionary<string, IOpenApiExtension>();

        swaggerDoc.Extensions["x-tagGroups"] = new JsonNodeExtension(new JsonArray
        {
            new JsonObject
            {
                ["name"] = "Endpoints",
                ["tags"] = new JsonArray { ImportNotificationsTag },
            },
        });
    }
}
