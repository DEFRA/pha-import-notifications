#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Finalisation
{
    [JsonPropertyName("finalState")]
    public required int FinalState { get; init; }

    [JsonPropertyName("manualAction")]
    public required bool ManualAction { get; init; }
}
