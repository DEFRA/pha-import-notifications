using System.Text.Json;
using Defra.PhaImportNotifications.Api.JsonApi;
using Defra.PhaImportNotifications.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

// ReSharper disable ClassNeverInstantiated.Local

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class JsonApiClientTests(WireMockContext context) : WireMockTestBase(context)
{
    private JsonApiClient Subject { get; } = new(context.HttpClient, NullLogger<JsonApiClient>.Instance);

    [Fact]
    public async Task Get_Many_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response.Create().WithStatusCode(StatusCodes.Status200OK).WithBodyFromFile("JsonApi\\get-people.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        document.Links?.Self.Should().Be("/api/people");
        document.Links?.First.Should().Be("/api/people");
        document.Links?.Last.Should().Be("/api/people");
        document.Links?.Next.Should().BeNull();
        document.Links?.Prev.Should().BeNull();

        var first = document.Data.ManyValue?[0];
        first.Should().NotBeNull();
        first?.Type.Should().Be("people");
        first?.Id.Should().Be("1");
        first?.Links?.Self.Should().Be("/api/people/1");

        first?.Relationships.Should().NotBeNull();
        first?.Relationships.Should().ContainKey("books");

        var firstBooks = first?.Relationships?["books"];
        firstBooks.Should().NotBeNull();
        firstBooks?.Links?.Self.Should().Be("/api/people/1/relationships/books");
        firstBooks?.Links?.Related.Should().Be("/api/people/1/books");

        document.Meta?["total"].Should().BeOfType<JsonElement>().Which.GetInt32().Should().Be(3);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Get_Many_WhenOk_AndListOfPeople_ShouldSucceed(bool idIsString)
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response.Create().WithStatusCode(StatusCodes.Status200OK).WithBodyFromFile("JsonApi\\get-people.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        if (idIsString)
        {
            var people = document.GetDataAsList<Person<string>>().ToList();
            await Verify(people).UseParameters(idIsString).UseStrictJson();
        }
        else
        {
            var people = document.GetDataAsList<Person<int>>().ToList();
            await Verify(people).UseParameters(idIsString).UseStrictJson();
        }
    }

    [Fact]
    public async Task Get_Many_WhenOk_AndListOfPeople_IncludeBooks_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("JsonApi\\get-people-include-books.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        var people = document.GetDataAsList<Person<int>>().ToList();

        var person = people[0];

        var books = document.GetIncludedAsList<Book<int>>("books", person.Id);

        await Verify(books);

        books = document.GetIncludedAsList<Book<int>>("books", 999);

        books.Should().BeEmpty();
    }

    [Fact]
    public async Task Get_Single_WhenOk_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response.Create().WithStatusCode(StatusCodes.Status200OK).WithBodyFromFile("JsonApi\\get-person.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        document.Links?.Self.Should().Be("/api/people/1");
        document.Links?.First.Should().BeNull();
        document.Links?.Last.Should().BeNull();
        document.Links?.Next.Should().BeNull();
        document.Links?.Prev.Should().BeNull();

        var first = document.Data.SingleValue;
        first.Should().NotBeNull();
        first?.Type.Should().Be("people");
        first?.Id.Should().Be("1");
        first?.Links?.Self.Should().Be("/api/people/1");

        first?.Relationships.Should().NotBeNull();
        first?.Relationships.Should().ContainKey("books");

        var firstBooks = first?.Relationships?["books"];
        firstBooks.Should().NotBeNull();
        firstBooks?.Links?.Self.Should().Be("/api/people/1/relationships/books");
        firstBooks?.Links?.Related.Should().Be("/api/people/1/books");

        document.Meta?["total"].Should().BeOfType<JsonElement>().Which.GetInt32().Should().Be(3);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Get_Single_WhenOk_AndSinglePerson_ShouldSucceed(bool idIsString)
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response.Create().WithStatusCode(StatusCodes.Status200OK).WithBodyFromFile("JsonApi\\get-person.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        if (idIsString)
        {
            var person = document.GetDataAs<Person<string>>();
            await Verify(person).UseParameters(idIsString).UseStrictJson();
        }
        else
        {
            var person = document.GetDataAs<Person<int>>();
            await Verify(person).UseParameters(idIsString).UseStrictJson();
        }
    }

    [Fact]
    public async Task Get_Single_WhenOk_AndSinglePerson_IncludeBooks_ShouldSucceed()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("JsonApi\\get-person-include-books.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        var person = document.GetDataAs<Person<int>>()!;

        var books = document.GetIncludedAsList<Book<int>>("books", person.Id);

        await Verify(books);

        books = document.GetIncludedAsList<Book<int>>("books", 999);

        books.Should().BeEmpty();
    }

    [Fact]
    public async Task Get_WhenNotOk_ShouldIncludeErrors()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status400BadRequest)
                    .WithBodyFromFile("JsonApi\\get-errors.json")
            );

        var document = await Subject.Get(new RequestUri("get"), CancellationToken.None);

        document.Should().NotBeNull();
        document.Errors.Should().NotBeNull();
        document.Errors?.Should().ContainSingle();
        document.Errors?[0].Id.Should().Be("6300efa9-7822-4e00-a895-03b7756201cb");
        document.Errors?[0].Status.Should().Be("400");
        document.Errors?[0].Title.Should().Be("The specified filter is invalid.");
        document.Errors?[0].Source?.Parameter.Should().Be("filter");
    }

    [Fact]
    public async Task Get_WhenNotOk_AndCannotDeserialize_ShouldThrow()
    {
        WireMock
            .Given(Request.Create().WithPath("/get").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(StatusCodes.Status500InternalServerError).WithBody("null"));

        var act = async () => await Subject.Get(new RequestUri("get"), CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Could not deserialize JSON");
    }

    [Fact]
    public async Task Get_WithFilter_ShouldSucceed()
    {
        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath("/get")
                    .UsingGet()
                    .WithParam("filter", MatchBehaviour.AcceptOnMatch, "equals(name,'Some Name')")
            )
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("JsonApi\\get-people-include-books.json")
            );

        var document = await Subject.Get(
            new RequestUri(
                "get",
                new FilterExpression(
                    LogicalOperator.And,
                    [new ComparisonExpression(ComparisonOperator.Equals, "name", "Some Name")]
                )
            ),
            CancellationToken.None
        );

        document.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_WithFields_ShouldSucceed()
    {
        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath("/get")
                    .UsingGet()
                    .WithParam("fields[people]", MatchBehaviour.AcceptOnMatch, "name")
            )
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("JsonApi\\get-people-include-books.json")
            );

        var document = await Subject.Get(
            new RequestUri(
                "get",
                new FilterExpression(
                    LogicalOperator.And,
                    [new ComparisonExpression(ComparisonOperator.Equals, "name", "Some Name")]
                )
            ),
            CancellationToken.None
        );

        document.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_WithPageSize_ShouldSucceed()
    {
        WireMock
            .Given(
                Request
                    .Create()
                    .WithPath("/get")
                    .UsingGet()
                    .WithParam("page[size]", MatchBehaviour.AcceptOnMatch, "100")
            )
            .RespondWith(
                Response
                    .Create()
                    .WithStatusCode(StatusCodes.Status200OK)
                    .WithBodyFromFile("JsonApi\\get-people-include-books.json")
            );

        var document = await Subject.Get(new RequestUri("get", PageSize: 100), CancellationToken.None);

        document.Should().NotBeNull();
    }

    private record Person<T>(T Id, string Name);

    private record Book<T>(T Id, string Title, int PublishYear);
}
