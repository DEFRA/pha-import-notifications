#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Document
{
    [JsonPropertyName("documentCode")]
    public string? DocumentCode { get; init; }

    [JsonPropertyName("documentReference")]
    public string? DocumentReference { get; init; }

    [JsonPropertyName("documentStatus")]
    public string? DocumentStatus { get; init; }

    [JsonPropertyName("documentControl")]
    public string? DocumentControl { get; init; }

    [JsonPropertyName("documentQuantity")]
    public decimal? DocumentQuantity { get; init; }
}
