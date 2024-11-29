#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class NotificationResourceData
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("attributes")]
    public required ImportNotification Attributes { get; init; }

    [JsonPropertyName("relationships")]
    public required NotificationJsonApiTdmRelationships Relationships { get; init; }
}
