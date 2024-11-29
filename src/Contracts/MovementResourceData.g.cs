#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class MovementResourceData
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("attributes")]
    public required Notification Attributes { get; init; }

    [JsonPropertyName("relationships")]
    public required MovementJsonApiTdmRelationships Relationships { get; init; }
}
