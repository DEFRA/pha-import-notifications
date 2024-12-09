using Defra.PhaImportNotifications.Api.JsonApi;
using FluentAssertions;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class FilterTests
{
    [Theory]
    [InlineData(ComparisonOperator.Equals, "value1", "equals(field1,'value1')")]
    [InlineData(ComparisonOperator.Equals, "value'1", "equals(field1,'value''1')")]
    [InlineData(ComparisonOperator.GreaterThan, "value1", "greaterThan(field1,'value1')")]
    [InlineData(ComparisonOperator.GreaterThan, "value'1", "greaterThan(field1,'value''1')")]
    [InlineData(ComparisonOperator.LessThan, "value1", "lessThan(field1,'value1')")]
    [InlineData(ComparisonOperator.LessThan, "value'1", "lessThan(field1,'value''1')")]
    public void ComparisonExpression_ToString_AsExpected(ComparisonOperator @operator, string value, string expected)
    {
        var subject = new ComparisonExpression(@operator, "field1", value);

        subject.ToString().Should().Be(expected);
    }

    [Fact]
    public void FilterExpression_NoComparisons()
    {
        var subject = new FilterExpression(LogicalOperator.And, []);

        subject.ToString().Should().Be(string.Empty);
    }

    [Fact]
    public void FilterExpression_SingleComparison()
    {
        var subject = new FilterExpression(
            LogicalOperator.And,
            [new ComparisonExpression(ComparisonOperator.Equals, "field1", "value1")]
        );

        subject.ToString().Should().Be("equals(field1,'value1')");
    }

    [Fact]
    public void FilterExpression_MultipleComparison()
    {
        var subject = new FilterExpression(
            LogicalOperator.And,
            [
                new ComparisonExpression(ComparisonOperator.Equals, "field1", "value1"),
                new ComparisonExpression(ComparisonOperator.Equals, "field2", "value2"),
            ]
        );

        subject.ToString().Should().Be("and(equals(field1,'value1'),equals(field2,'value2'))");
    }

    [Fact]
    public void FilterExpression_Nested()
    {
        var subject = new FilterExpression(
            LogicalOperator.And,
            [new ComparisonExpression(ComparisonOperator.Equals, "field1", "value1")],
            [
                new FilterExpression(
                    LogicalOperator.Or,
                    [
                        new ComparisonExpression(ComparisonOperator.GreaterThan, "field2", "field2-val1"),
                        new ComparisonExpression(ComparisonOperator.LessThan, "field2", "field2-val2"),
                    ]
                ),
            ]
        );

        subject
            .ToString()
            .Should()
            .Be("and(equals(field1,'value1'),or(greaterThan(field2,'field2-val1'),lessThan(field2,'field2-val2')))");
    }

    [Theory]
    [InlineData(new[] { "value1" }, "any(field1,'value1')")]
    [InlineData(new[] { "value'1" }, "any(field1,'value''1')")]
    [InlineData(new[] { "value1", "value2", "value3" }, "any(field1,'value1','value2','value3')")]
    [InlineData(new string[] { }, "")]
    public void AnyExpression_ToString_AsExpected(string[] values, string expected)
    {
        var subject = new AnyExpression("field1", values);

        subject.ToString().Should().Be(expected);
    }
}
