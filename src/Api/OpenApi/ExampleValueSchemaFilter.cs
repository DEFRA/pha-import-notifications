using Defra.PhaImportNotifications.Contracts;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Defra.PhaImportNotifications.Api.OpenApi;

public class ExampleValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var exampleValueAttributes =
            context.MemberInfo?.GetCustomAttributes(false).OfType<ExampleValueAttribute>().ToList() ?? [];

        if (exampleValueAttributes.Count != 0)
        {
            schema.Description +=
                $"{Environment.NewLine}{Environment.NewLine}"
                + "Enum values represent current example values but additional values may also be returned as"
                + " underlying systems evolve. Consuming clients should cater for this possibility.";

            var values = exampleValueAttributes.Select(a => a.Value).Where(v => !string.IsNullOrWhiteSpace(v));
            foreach (var value in values)
            {
                schema.Enum.Add(new OpenApiString(value));
            }
        }
    }
}
