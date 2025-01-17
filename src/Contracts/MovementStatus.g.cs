#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record MovementStatus
{
    [JsonPropertyName("chedTypes")]
    public List<ImportNotificationTypeEnum>? ChedTypes { get; init; }

    [JsonPropertyName("linkStatus")]
    public required LinkStatusEnum LinkStatus { get; init; }

    [JsonPropertyName("linkStatusDescription")]
    public string? LinkStatusDescription { get; init; }

    [JsonPropertyName("status")]
    public MovementStatusEnum? Status { get; init; }

    [JsonPropertyName("segment")]
    public MovementSegmentEnum? Segment { get; init; }
}
