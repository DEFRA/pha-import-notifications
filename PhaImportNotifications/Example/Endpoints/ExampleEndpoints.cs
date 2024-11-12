using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PhaImportNotifications.Example.Models;
using PhaImportNotifications.Example.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PhaImportNotifications.Example.Endpoints;

[ExcludeFromCodeCoverage]
[SwaggerTag("Example endpoints")]
public static class ExampleEndpoints
{
    public static void UseExampleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("example", Create);

        app.MapGet("example", GetAll);

        app.MapGet("example/{name}", GetByName);

        app.MapPut("example/{name}", Update);

        app.MapDelete("example/{name}", Delete);
    }

    [HttpPost]
    private static async Task<IResult> Create(
        ExampleModel example,
        IExamplePersistence examplePersistence,
        IValidator<ExampleModel> validator
    )
    {
        var validationResult = await validator.ValidateAsync(example);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var created = await examplePersistence.CreateAsync(example);
        if (!created)
            return Results.BadRequest(
                new List<ValidationFailure> { new("Example", "An example record with this name already exists") }
            );

        return Results.Created($"/example/{example.Name}", example);
    }

    [HttpGet]
    private static async Task<IResult> GetAll(IExamplePersistence examplePersistence, string? searchTerm)
    {
        if (searchTerm is not null && !string.IsNullOrWhiteSpace(searchTerm))
        {
            var matched = await examplePersistence.SearchByValueAsync(searchTerm);
            return Results.Ok(matched);
        }

        var matches = await examplePersistence.GetAllAsync();
        return Results.Ok(matches);
    }

    [HttpGet]
    private static async Task<IResult> GetByName(
        [FromRoute, SwaggerParameter("The name to fetch", Required = true)] string name,
        IExamplePersistence examplePersistence
    )
    {
        var example = await examplePersistence.GetByExampleName(name);
        return example is not null ? Results.Ok(example) : Results.NotFound();
    }

    [HttpPut]
    private static async Task<IResult> Update(
        string name,
        ExampleModel example,
        IExamplePersistence examplePersistence,
        IValidator<ExampleModel> validator
    )
    {
        example.Name = name;
        var validationResult = await validator.ValidateAsync(example);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var updated = await examplePersistence.UpdateAsync(example);
        return updated ? Results.Ok(example) : Results.NotFound();
    }

    [HttpDelete]
    private static async Task<IResult> Delete(string name, IExamplePersistence examplePersistence)
    {
        var deleted = await examplePersistence.DeleteAsync(name);
        return deleted ? Results.Ok() : Results.NotFound();
    }
}
