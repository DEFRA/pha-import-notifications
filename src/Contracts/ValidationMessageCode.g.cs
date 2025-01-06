#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ValidationMessageCode
{
    [JsonPropertyName("field")]
    [Description("Field")]
    public string? Field { get; init; }

    [JsonPropertyName("code")]
    [Description("Code")]
    public string? Code { get; init; }
}
