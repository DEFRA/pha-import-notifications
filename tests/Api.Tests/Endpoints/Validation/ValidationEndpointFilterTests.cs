using Defra.PhaImportNotifications.Api.Endpoints.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public class ValidationEndpointFilterTests
{
    private record TestArgs(string? Name);

    private class TestValidationEndpointFilter : ValidationEndpointFilter<TestArgs>
    {
        public TestValidationEndpointFilter()
        {
            RuleFor(x => x.Name).NotNull();
        }

        protected override int ArgumentIndex => 1;
    }

    private static ValueTask<object?> OkResult(EndpointFilterInvocationContext endpointFilterInvocationContext) =>
        new(new OkResult());

    [Fact]
    public async Task ShouldReturnBadRequestWhenInvalid()
    {
        var filter = new TestValidationEndpointFilter();
        var arg0 = new object();
        var arg1 = new TestArgs(null);
        var context = EndpointFilterInvocationContext.Create(new DefaultHttpContext(), arg0, arg1);

        var result = await filter.InvokeAsync(context, OkResult);

        result.Should().NotBeNull();
        result.Should().BeOfType<ProblemHttpResult>();
        var problem = (ProblemHttpResult)result!;
        problem.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ShouldCallNextWhenValid()
    {
        var filter = new TestValidationEndpointFilter();
        var arg0 = new object();
        var arg1 = new TestArgs("John Smith");
        var context = EndpointFilterInvocationContext.Create(new DefaultHttpContext(), arg0, arg1);

        var result = await filter.InvokeAsync(context, OkResult);

        result.Should().NotBeNull();
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task ShouldThrowArgumentNotFoundAtIndexException_When_ArgumentIsNotFoundAtIndex()
    {
        var filter = new TestValidationEndpointFilter();
        var arg0 = new TestArgs("John Smith");
        var arg1 = new object();
        var context = EndpointFilterInvocationContext.Create(new DefaultHttpContext(), arg0, arg1);

        await Assert.ThrowsAsync<TestValidationEndpointFilter.ArgumentNotFoundAtIndexException>(
            async () => await filter.InvokeAsync(context, OkResult)
        );
    }
}
