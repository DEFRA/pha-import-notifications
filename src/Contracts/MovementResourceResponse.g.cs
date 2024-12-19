#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class MovementResourceResponse
{
    [JsonPropertyName("data")]
    public Movement? Data { get; init; }
}
