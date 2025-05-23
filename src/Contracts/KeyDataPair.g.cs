#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record KeyDataPair
{
    [JsonPropertyName("key")]
    public string? Key { get; init; }

    [JsonPropertyName("data")]
    public string? Data { get; init; }
}
