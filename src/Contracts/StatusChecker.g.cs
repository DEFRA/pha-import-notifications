#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record StatusChecker
{
    [JsonPropertyName("allMatch")]
    public required bool AllMatch { get; init; }

    [JsonPropertyName("anyMatch")]
    public required bool AnyMatch { get; init; }

    [JsonPropertyName("allNoMatch")]
    public required bool AllNoMatch { get; init; }

    [JsonPropertyName("anyNoMatch")]
    public required bool AnyNoMatch { get; init; }

    [JsonPropertyName("allHold")]
    public required bool AllHold { get; init; }

    [JsonPropertyName("anyHold")]
    public required bool AnyHold { get; init; }

    [JsonPropertyName("allRefuse")]
    public required bool AllRefuse { get; init; }

    [JsonPropertyName("anyRefuse")]
    public required bool AnyRefuse { get; init; }

    [JsonPropertyName("allRelease")]
    public required bool AllRelease { get; init; }

    [JsonPropertyName("anyRelease")]
    public required bool AnyRelease { get; init; }
}
