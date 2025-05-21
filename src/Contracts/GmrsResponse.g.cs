#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record GmrsResponse
{
    [JsonPropertyName("gmrs")]
    public required List<GmrResponse> Gmrs { get; init; }
}
