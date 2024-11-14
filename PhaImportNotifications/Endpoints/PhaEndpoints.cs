using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PhaImportNotifications.Endpoints;

public static class PhaEndpoints
{
    public static void UsePhaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("hello/world", HelloWorld).WithTags("Example Endpoints");
    }

    [HttpGet]
    [SwaggerOperation(Description = "An endpoint for hello world", Summary = "Hello World")]
    [SwaggerResponse(200, "Returns Hello World", typeof(HelloWorldResponse))]
    public static HelloWorldResponse HelloWorld()
    {
        return new HelloWorldResponse();
    }

    [SwaggerSchema("The hello world response")]
    public class HelloWorldResponse
    {
        /// <example>Hello World</example>
        [StringLength(11)]
        [RegularExpression("^Hello World$")]
        [SwaggerSchema("A string consisting of Hello World")]
        public string Response { get; } = "Hello World";
    }
}
