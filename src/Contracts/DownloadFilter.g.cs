#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DownloadFilter
{
    [JsonPropertyName("mrns")]
    public List<string>? Mrns { get; init; }

    [JsonPropertyName("cheds")]
    public List<Ched>? Cheds { get; init; }
}
