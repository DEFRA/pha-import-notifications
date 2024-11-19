using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.SwashbuckleFilters;

[ExcludeFromCodeCoverage]
public class AddSwashbuckleHeaders : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionAttributes = context
            .MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<HttpMethodAttribute>();

        if (actionAttributes == null || !actionAttributes.Any())
            return;

        foreach (var (statusCode, _) in operation.Responses)
        {
            operation
                .Responses[statusCode]
                .Headers.Add(
                    "Retry-After",
                    new OpenApiHeader
                    {
                        Description =
                            $"How long to wait in seconds before retrying {operation.OperationId}: {statusCode}",
                        Example = new OpenApiInteger(60),
                        Schema = new OpenApiSchema
                        {
                            Type = "integer",
                            Format = "int64",
                            Minimum = 1,
                            Maximum = 99999,
                        },
                    }
                );

            operation
                .Responses[statusCode]
                .Headers.Add(
                    "X-RateLimit-Limit",
                    new OpenApiHeader
                    {
                        Description = $"Remaining API request rate limit for {operation.OperationId}: {statusCode}",
                        Example = new OpenApiInteger(100),
                        Schema = new OpenApiSchema
                        {
                            Type = "integer",
                            Format = "int64",
                            Minimum = 1,
                            Maximum = 99999,
                        },
                    }
                );

            operation
                .Responses[statusCode]
                .Headers.Add(
                    "X-RateLimit-Reset",
                    new OpenApiHeader
                    {
                        Description =
                            $"Number of seconds until the quota resets for {operation.OperationId}: {statusCode}",
                        Example = new OpenApiInteger(60),
                        Schema = new OpenApiSchema
                        {
                            Type = "integer",
                            Format = "int64",
                            Minimum = 0,
                            Maximum = 60 * 60 * 24,
                        },
                    }
                );
        }
    }
}
