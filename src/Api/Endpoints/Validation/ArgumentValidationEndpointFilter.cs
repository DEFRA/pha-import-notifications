using FluentValidation;
using FluentValidation.Results;

namespace Defra.PhaImportNotifications.Api.Endpoints.Validation;

public abstract class ValidationEndpointFilter<TArgument> : AbstractValidator<TArgument>, IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            var requestParams = context.GetArgument<TArgument>(ArgumentIndex);
            var validationResult = await ValidateAsync(requestParams);

            if (validationResult.IsValid)
                return await next(context);

            return Results.ValidationProblem(GroupErrors(validationResult.Errors));
        }
        catch (Exception ex) when (ex is InvalidCastException or ArgumentOutOfRangeException)
        {
            throw new ArgumentNotFoundAtIndexException(typeof(TArgument), ArgumentIndex);
        }
    }

    public class ArgumentNotFoundAtIndexException(Type type, int index)
        : Exception($"Argument for type '{type}' not found at index '{index}'") { }

    private static IEnumerable<KeyValuePair<string, string[]>> GroupErrors(List<ValidationFailure> failures) =>
        failures
            .GroupBy(e => e.PropertyName)
            .Select(e => new KeyValuePair<string, string[]>(e.Key, e.Select(error => error.ErrorMessage).ToArray()));

    protected abstract int ArgumentIndex { get; }
}
