#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ResourceLink
{
    [JsonPropertyName("self")]
    public string? Self { get; init; }
}
