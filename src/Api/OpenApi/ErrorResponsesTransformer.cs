using System.Net.Mime;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Api.OpenApi;

public class ErrorResponsesTransformer : IOpenApiOperationTransformer
{
    private const string StringType = "string";

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        operation.Responses.Add(StatusCodes.Status400BadRequest.ToString(), CreateGenericResponse("Bad Request"));
        operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), CreateGenericResponse("Unauthorized"));
        operation.Responses.Add(
            StatusCodes.Status429TooManyRequests.ToString(),
            CreateGenericResponse("Too Many Requests")
        );
        operation.Responses.Add(
            StatusCodes.Status500InternalServerError.ToString(),
            CreateGenericResponse("Internal Server Error")
        );

        return Task.CompletedTask;
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
