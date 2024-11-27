#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class CatchCertificates
{
    [JsonPropertyName("certificateNumber")]
    [Description("The catch certificate number")]
    public string? CertificateNumber { get; init; }

    [JsonPropertyName("weight")]
    [Description("The catch certificate weight number")]
    public decimal? Weight { get; init; }
}
