#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class GmrResourceResponse
{
    [JsonPropertyName("data")]
    public required GmrResourceData Data { get; init; }
}
