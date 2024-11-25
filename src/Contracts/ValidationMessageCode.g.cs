#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ValidationMessageCode
{
    [JsonPropertyName("field")]
    [Description("Field")]
    public string? Field { get; set; }

    [JsonPropertyName("code")]
    [Description("Code")]
    public string? Code { get; set; }
}
