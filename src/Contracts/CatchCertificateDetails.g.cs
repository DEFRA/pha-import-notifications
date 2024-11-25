#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class CatchCertificateDetails
{
    [JsonPropertyName("catchCertificateId")]
    [Description("The UUID of the catch certificate")]
    public string? CatchCertificateId { get; set; }

    [JsonPropertyName("catchCertificateReference")]
    [Description("Catch certificate reference")]
    public string? CatchCertificateReference { get; set; }

    [JsonPropertyName("issuedOn")]
    [Description("Catch certificate date of issue")]
    public DateTime? IssuedOn { get; set; }

    [JsonPropertyName("flagState")]
    [Description("Catch certificate flag state of catching vessel(s)")]
    public string? FlagState { get; set; }

    [JsonPropertyName("species")]
    [Description("List of species imported under this catch certificate")]
    public List<string>? Species { get; set; }
}
