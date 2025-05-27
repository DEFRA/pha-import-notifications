using Defra.PhaImportNotifications.Api.Extensions;

namespace Defra.PhaImportNotifications.Tests.Api.Unit.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void NotNull_ReturnsNotNull()
    {
        var subject = new List<string?> { "string1", null, "string2" };

        subject.NotNull().Should().BeEquivalentTo(new List<string> { "string1", "string2" });
    }

    [Fact]
    public void ThrowIfAnyNull_WhenOneIsNull_ShouldThrow()
    {
        var subject = new List<string?> { "string1", null, "string2" };

        var act = () => subject.ThrowIfAnyNull("One is null");

        act.Should().Throw<InvalidOperationException>().WithMessage("One is null");
    }
}
