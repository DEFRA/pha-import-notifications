using System.Text.Json;
using System.Text.Json.Serialization;
using Defra.PhaImportNotifications.Api.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Json;

namespace Defra.PhaImportNotifications.Api.Tests.Configuration;

public class SerializerOptionsTests
{
    [Fact]
    public void Configure_AsExpected()
    {
        var options = SerializerOptions.Configure(new JsonOptions());

        options.SerializerOptions.PropertyNameCaseInsensitive.Should().BeTrue();
        options.SerializerOptions.PropertyNamingPolicy.Should().Be(JsonNamingPolicy.CamelCase);
        options.SerializerOptions.NumberHandling.Should().Be(JsonNumberHandling.AllowReadingFromString);
        options.SerializerOptions.Converters.Should().Contain(x => x.GetType() == typeof(JsonStringEnumConverter));
    }
}
