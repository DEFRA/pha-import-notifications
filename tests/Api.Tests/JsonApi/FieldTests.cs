using Defra.PhaImportNotifications.Api.JsonApi;

namespace Defra.PhaImportNotifications.Api.Tests.JsonApi;

public class FieldTests
{
    [Fact]
    public void Fields_ShouldBeAsExpected()
    {
        var subject = new FieldExpression("resource", ["field1", "field2"]);

        subject.ToString().Should().Be("fields[resource]=field1,field2");
    }

    [Fact]
    public void NoFields_ShouldBeEmpty()
    {
        var subject = new FieldExpression("resource", []);

        subject.ToString().Should().Be(string.Empty);
    }
}
