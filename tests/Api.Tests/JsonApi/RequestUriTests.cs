using Defra.PhaImportNotifications.Api.JsonApi;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class RequestUriTests
{
    [Fact]
    public void WhenPath_ShouldBePath()
    {
        var subject = new RequestUri("path");

        subject.ToString().Should().Be("path");
    }

    [Fact]
    public void WhenFilter_ShouldIncludeFilter()
    {
        var subject = new RequestUri(
            "path",
            new FilterExpression(LogicalOperator.And, [new AnyExpression("field1", ["value1", "value2"])])
        );

        Uri.UnescapeDataString(subject.ToString()).Should().Be("path?filter=any(field1,'value1','value2')");
    }

    [Fact]
    public void WhenFields_ShouldIncludeFields()
    {
        var subject = new RequestUri("path", Fields: [new FieldExpression("resource1", ["field1", "field2"])]);

        Uri.UnescapeDataString(subject.ToString()).Should().Be("path?fields[resource1]=field1,field2");
    }

    [Fact]
    public void WhenMultipleFields_ShouldIncludeFields()
    {
        var subject = new RequestUri(
            "path",
            Fields:
            [
                new FieldExpression("resource1", ["field1", "field2"]),
                new FieldExpression("resource2", ["field1", "field2"]),
            ]
        );

        Uri.UnescapeDataString(subject.ToString())
            .Should()
            .Be("path?fields[resource1]=field1,field2&fields[resource2]=field1,field2");
    }

    [Fact]
    public void WhenFields_WithNoFields_ShouldSkip()
    {
        var subject = new RequestUri("path", Fields: [new FieldExpression("resource1", [])]);

        Uri.UnescapeDataString(subject.ToString()).Should().Be("path");
    }

    [Fact]
    public void WhenFilterAndFields_WithNoFields_ShouldSkip()
    {
        var subject = new RequestUri(
            "path",
            new FilterExpression(LogicalOperator.And, [new AnyExpression("field1", ["value1", "value2"])]),
            [new FieldExpression("resource1", [])]
        );

        Uri.UnescapeDataString(subject.ToString()).Should().Be("path?filter=any(field1,'value1','value2')");
    }

    [Fact]
    public void WhenFilterAndFields_ShouldIncludeBoth()
    {
        var subject = new RequestUri(
            "path",
            new FilterExpression(LogicalOperator.And, [new AnyExpression("field1", ["value1", "value2"])]),
            [new FieldExpression("resource1", ["field1", "field2"])]
        );

        Uri.UnescapeDataString(subject.ToString())
            .Should()
            .Be("path?filter=any(field1,'value1','value2')&fields[resource1]=field1,field2");
    }

    [Fact]
    public void WhenPageSize_ShouldIncludePageSize()
    {
        var subject = new RequestUri("path", PageSize: 100);

        Uri.UnescapeDataString(subject.ToString()).Should().Be("path?page[size]=100");
    }
}
