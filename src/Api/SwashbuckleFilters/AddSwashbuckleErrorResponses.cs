using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.SwashbuckleFilters;

[ExcludeFromCodeCoverage]
public class AddSwashbuckleErrorResponses : IOperationFilter
{
    private const string StringType = "string";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionAttributes = context
            .MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<HttpMethodAttribute>();

        if (actionAttributes == null || !actionAttributes.Any())
            return;

        operation.Responses.Add("400", CreateGenericResponse("Bad Request"));
        operation.Responses.Add("401", CreateGenericResponse("Unauthorized"));
        operation.Responses.Add("429", CreateGenericResponse("Too Many Requests"));
        operation.Responses.Add("500", CreateGenericResponse("Internal Server Error"));
    }

    private static OpenApiResponse CreateGenericResponse(string responseText)
    {
        return new OpenApiResponse
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                {
                    MediaTypeNames.Text.Plain,
                    new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Enum = [new OpenApiString(responseText)],
                            Example = new OpenApiString(responseText),
                            Type = StringType,
                        },
                    }
                },
            },
            Description = responseText,
        };
    }
}
