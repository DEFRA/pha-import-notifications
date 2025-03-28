#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Movement
{
    [JsonPropertyName("btmsStatus")]
    [JsonIgnore]
    public MovementStatus? BtmsStatus { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore]
    public string? Type { get; init; }

    [JsonPropertyName("clearanceRequests")]
    public List<CustomsClearanceRequest>? ClearanceRequests { get; init; }

    [JsonPropertyName("decisions")]
    public List<CustomsClearanceDecision>? Decisions { get; init; }

    [JsonPropertyName("alvsDecisionStatus")]
    [JsonIgnore]
    public AlvsDecisionStatus? AlvsDecisionStatus { get; init; }

    [JsonPropertyName("items")]
    [JsonIgnore]
    public List<Items>? Items { get; init; }

    [JsonPropertyName("updatedSource")]
    [Description("Date when the movement was last updated in ALVS")]
    public DateTime? UpdatedSource { get; init; }

    [JsonPropertyName("createdSource")]
    [Description("Date when the movement was created in ALVS")]
    public DateTime? CreatedSource { get; init; }

    [JsonPropertyName("updated")]
    [Description("Date when the movement was last updated")]
    public required DateTime Updated { get; init; }

    [JsonPropertyName("entryReference")]
    public string? EntryReference { get; init; }

    [JsonPropertyName("entryVersionNumber")]
    public required int EntryVersionNumber { get; init; }

    [JsonPropertyName("masterUcr")]
    public string? MasterUcr { get; init; }

    [JsonPropertyName("declarationPartNumber")]
    public int? DeclarationPartNumber { get; init; }

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

    [JsonPropertyName("auditEntries")]
    [JsonIgnore]
    public List<AuditEntry>? AuditEntries { get; init; }

    [JsonPropertyName("relationships")]
    [JsonIgnore]
    public MovementTdmRelationships? Relationships { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("created")]
    [Description("Date when the movement was created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("updatedEntity")]
    [Description("Date when the movement was updated or when related data was linked or updated")]
    public required DateTime UpdatedEntity { get; init; }

    [JsonPropertyName("finalised")]
    public DateTime? Finalised { get; init; }

    [JsonPropertyName("finalisedSource")]
    public DateTime? FinalisedSource { get; init; }

    [JsonPropertyName("finalisation")]
    public Finalisation? Finalisation { get; init; }
}
