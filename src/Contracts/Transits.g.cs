#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Transits
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }
}
