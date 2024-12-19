#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Header
{
    [JsonPropertyName("entryReference")]
    public string? EntryReference { get; init; }

    [JsonPropertyName("entryVersionNumber")]
    public int? EntryVersionNumber { get; init; }

    [JsonPropertyName("previousVersionNumber")]
    public int? PreviousVersionNumber { get; init; }

    [JsonPropertyName("declarationUcr")]
    public string? DeclarationUcr { get; init; }

    [JsonPropertyName("declarationPartNumber")]
    public string? DeclarationPartNumber { get; init; }

    [JsonPropertyName("declarationType")]
    public string? DeclarationType { get; init; }

    [JsonPropertyName("arrivesAt")]
    public DateTime? ArrivesAt { get; init; }

    [JsonPropertyName("submitterTurn")]
    public string? SubmitterTurn { get; init; }

    [JsonPropertyName("declarantId")]
    public string? DeclarantId { get; init; }

    [JsonPropertyName("declarantName")]
    public string? DeclarantName { get; init; }

    [JsonPropertyName("dispatchCountryCode")]
    public string? DispatchCountryCode { get; init; }

    [JsonPropertyName("goodsLocationCode")]
    public string? GoodsLocationCode { get; init; }

    [JsonPropertyName("masterUcr")]
    public string? MasterUcr { get; init; }

    [JsonPropertyName("decisionNumber")]
    public int? DecisionNumber { get; init; }
}
