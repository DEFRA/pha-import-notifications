#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ClearanceRequest
{
    [JsonPropertyName("externalCorrelationId")]
    public string? ExternalCorrelationId { get; init; }

    [JsonPropertyName("messageSentAt")]
    public required DateTime MessageSentAt { get; init; }

    [JsonPropertyName("externalVersion")]
    public int? ExternalVersion { get; init; }

    [JsonPropertyName("previousExternalVersion")]
    public int? PreviousExternalVersion { get; init; }

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

    [JsonPropertyName("commodities")]
    public List<Commodity>? Commodities { get; init; }
}
