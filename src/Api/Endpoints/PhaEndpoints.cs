using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class PhaEndpoints
{
    public static void MapPhaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("hello/world", HelloWorld)
            .WithName("HelloWorld")
            .WithTags("Example Endpoints")
            .WithSummary("Hello World")
            .WithDescription("An endpoint for hello world")
            .Produces<HelloWorldResponse>();
    }

    [HttpGet]
    public static HelloWorldResponse HelloWorld()
    {
        return new HelloWorldResponse();
    }

    /// <summary>
    /// The hello world response
    /// </summary>
    public class HelloWorldResponse
    {
        /// <summary>A string consisting of Hello World</summary>
        /// <example>Hello World</example>
        [StringLength(11)]
        [RegularExpression("^Hello World$")]
        public string Response { get; } = "Hello World";
    }
}
