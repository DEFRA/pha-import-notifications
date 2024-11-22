using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class IdentificationDetails
{
    [JsonPropertyName("identificationDetail")]
    [Description("Identification detail")]
    public string? IdentificationDetail { get; init; }

    [JsonPropertyName("identificationDescription")]
    [Description("Identification description")]
    public string? IdentificationDescription { get; init; }
}
