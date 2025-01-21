#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Ched
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; init; }
}
