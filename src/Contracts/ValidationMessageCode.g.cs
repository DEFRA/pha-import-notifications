#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ValidationMessageCode
{
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("field")]
    [Description("Field")]
    public string? Field { get; init; }

    [JsonPropertyName("code")]
    [Description("Code")]
    public string? Code { get; init; }
}
