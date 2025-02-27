#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsClearanceRequest
{
    [JsonPropertyName("serviceHeader")]
    [JsonIgnore]
    public ServiceHeader? ServiceHeader { get; init; }

    [JsonPropertyName("header")]
    public Header? Header { get; init; }

    [JsonPropertyName("items")]
    public List<Items>? Items { get; init; }
}
