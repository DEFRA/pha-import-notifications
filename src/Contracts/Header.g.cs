#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Header
{
    [JsonPropertyName("entryReference")]
    public string? EntryReference { get; init; }

    [JsonPropertyName("entryVersionNumber")]
    public int? EntryVersionNumber { get; init; }

    [JsonPropertyName("previousVersionNumber")]
    public int? PreviousVersionNumber { get; init; }

    [JsonPropertyName("declarationUcr")]
    [JsonIgnore]
    public string? DeclarationUcr { get; init; }

    [JsonPropertyName("declarationPartNumber")]
    [JsonIgnore]
    public string? DeclarationPartNumber { get; init; }

    [JsonPropertyName("declarationType")]
    [JsonIgnore]
    public string? DeclarationType { get; init; }

    [JsonPropertyName("arrivesAt")]
    public DateTime? ArrivesAt { get; init; }

    [JsonPropertyName("submitterTurn")]
    [JsonIgnore]
    public string? SubmitterTurn { get; init; }

    [JsonPropertyName("declarantId")]
    [JsonIgnore]
    public string? DeclarantId { get; init; }

    [JsonPropertyName("declarantName")]
    [JsonIgnore]
    public string? DeclarantName { get; init; }

    [JsonPropertyName("dispatchCountryCode")]
    public string? DispatchCountryCode { get; init; }

    [JsonPropertyName("goodsLocationCode")]
    public string? GoodsLocationCode { get; init; }

    [JsonPropertyName("masterUcr")]
    [JsonIgnore]
    public string? MasterUcr { get; init; }
}
