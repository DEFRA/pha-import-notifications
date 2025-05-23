#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record GmrResponse
{
    [JsonPropertyName("gmr")]
    public required Gmr Gmr { get; init; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }
}
