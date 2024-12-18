using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public static class ExampleEndpoints
{
    public static void MapExampleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("hello/world", HelloWorld).ExcludeFromDescription();
    }

    [HttpGet]
    private static HelloWorldResponse HelloWorld()
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
