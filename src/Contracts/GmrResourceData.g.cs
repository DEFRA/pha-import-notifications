#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class GmrResourceData
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("attributes")]
    public required Gmr Attributes { get; init; }

    [JsonPropertyName("relationships")]
    public required GrmJsonApiTdmRelationships Relationships { get; init; }
}
