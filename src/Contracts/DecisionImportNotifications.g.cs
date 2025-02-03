#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record DecisionImportNotifications
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("version")]
    public int? Version { get; init; }

    [JsonPropertyName("created")]
    public required DateTime Created { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }

    [JsonPropertyName("updatedEntity")]
    public required DateTime UpdatedEntity { get; init; }

    [JsonPropertyName("createdSource")]
    public required DateTime CreatedSource { get; init; }

    [JsonPropertyName("updatedSource")]
    public required DateTime UpdatedSource { get; init; }
}
