using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ApprovedEstablishment
{
    [JsonPropertyName("id")]
    [Description("ID")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of approved establishment")]
    public string? Name { get; init; }

    [JsonPropertyName("country")]
    [Description("Country of approved establishment")]
    public string? Country { get; init; }

    [JsonPropertyName("types")]
    [Description("Types of approved establishment")]
    public List<string>? Types { get; init; }

    [JsonPropertyName("approvalNumber")]
    [Description("Approval number")]
    public string? ApprovalNumber { get; init; }

    [JsonPropertyName("section")]
    [Description("Section of approved establishment")]
    public string? Section { get; init; }
}
