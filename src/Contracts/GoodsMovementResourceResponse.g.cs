#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record GoodsMovementResourceResponse
{
    [JsonPropertyName("data")]
    public Gmr? Data { get; init; }
}
