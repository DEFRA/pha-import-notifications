using Defra.PhaImportNotifications.Api.JsonApi;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class FieldTests
{
    [Fact]
    public void ToString_Fields_ShouldBeAsExpected()
    {
        var subject = new FieldExpression("resource", ["field1", "field2"]);

        subject.ToString().Should().Be("fields[resource]=field1,field2");
    }

    [Fact]
    public void ToString_NoFields_ShouldBeEmpty()
    {
        var subject = new FieldExpression("resource", []);

        subject.ToString().Should().Be(string.Empty);
    }

    [Fact]
    public void ToParts_Fields_ShouldBeAsExpected()
    {
        var subject = new FieldExpression("resource", ["field1", "field2"]);

        var result = subject.ToParts();

        result.Should().NotBeNull();
        result?.Key.Should().Be("fields[resource]");
        result?.Value.Should().Be("field1,field2");
    }

    [Fact]
    public void ToParts_NoFields_ShouldBeEmpty()
    {
        var subject = new FieldExpression("resource", []);

        subject.ToParts().Should().BeNull();
    }
}
