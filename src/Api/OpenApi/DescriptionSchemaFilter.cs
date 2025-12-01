using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Defra.PhaImportNotifications.Api.OpenApi;

[ExcludeFromCodeCoverage]
public class DescriptionSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.ParameterInfo != null)
        {
            var descriptionAttributes = context.ParameterInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttributes.Length > 0)
            {
                var descriptionAttribute = (DescriptionAttribute)descriptionAttributes[0];
                schema.Description = descriptionAttribute.Description;
            }
        }

        if (context.MemberInfo != null)
        {
            var descriptionAttributes = context.MemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttributes.Length > 0)
            {
                var descriptionAttribute = (DescriptionAttribute)descriptionAttributes[0];
                schema.Description = descriptionAttribute.Description;
            }
        }

        if (context.Type != null)
        {
            var descriptionAttributes = context.Type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptionAttributes.Length > 0)
            {
                var descriptionAttribute = (DescriptionAttribute)descriptionAttributes[0];
                schema.Description = descriptionAttribute.Description;
            }
        }
    }
}
