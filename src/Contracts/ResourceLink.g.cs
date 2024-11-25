#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ResourceLink
{
    [JsonPropertyName("self")]
    public string? Self { get; init; }
}
