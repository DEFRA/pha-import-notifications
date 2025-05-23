#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Inspector
{
    [JsonPropertyName("name")]
    [Description("Name of inspector")]
    public string? Name { get; init; }

    [JsonPropertyName("phone")]
    [Description("Phone number of inspector")]
    public string? Phone { get; init; }

    [JsonPropertyName("email")]
    [Description("Email address of inspector")]
    public string? Email { get; init; }
}
