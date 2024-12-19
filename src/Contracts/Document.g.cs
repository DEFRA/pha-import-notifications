#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class Document
{
    [JsonPropertyName("documentCode")]
    public string? DocumentCode { get; init; }

    [JsonPropertyName("documentReference")]
    public string? DocumentReference { get; init; }

    [JsonPropertyName("documentStatus")]
    [JsonIgnore]
    public string? DocumentStatus { get; init; }

    [JsonPropertyName("documentControl")]
    [JsonIgnore]
    public string? DocumentControl { get; init; }

    [JsonPropertyName("documentQuantity")]
    [JsonIgnore]
    public decimal? DocumentQuantity { get; init; }
}
