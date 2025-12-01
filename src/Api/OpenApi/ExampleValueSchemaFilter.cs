using System.Text.Json.Nodes;
using Defra.PhaImportNotifications.Contracts;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Defra.PhaImportNotifications.Api.OpenApi;

public class ExampleValueSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        var open = schema as OpenApiSchema;

        var exampleValueAttributes =
            context.MemberInfo?.GetCustomAttributes(false).OfType<ExampleValueAttribute>().ToList() ?? [];

        if (exampleValueAttributes.Count != 0)
        {
            schema.Description +=
                $"{Environment.NewLine}{Environment.NewLine}"
                + "Enum values represent current example values but additional values may also be returned as"
                + " underlying systems evolve. Consuming clients should cater for this possibility.";

            var values = exampleValueAttributes.Select(a => a.Value).Where(v => !string.IsNullOrWhiteSpace(v));

            open!.Enum ??= new List<JsonNode>();

            foreach (var value in values)
            {
                open.Enum.Add(value);
            }
        }
    }
}
