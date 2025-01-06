#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Check
{
    [JsonPropertyName("checkCode")]
    public string? CheckCode { get; init; }

    [JsonPropertyName("departmentCode")]
    public string? DepartmentCode { get; init; }

    [JsonPropertyName("decisionCode")]
    public string? DecisionCode { get; init; }

    [JsonPropertyName("decisionsValidUntil")]
    public DateTime? DecisionsValidUntil { get; init; }

    [JsonPropertyName("decisionReasons")]
    public List<string>? DecisionReasons { get; init; }
}
