#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CatchCertificateDetails
{
    [JsonPropertyName("catchCertificateId")]
    [Description("The UUID of the catch certificate")]
    public string? CatchCertificateId { get; init; }

    [JsonPropertyName("catchCertificateReference")]
    [Description("Catch certificate reference")]
    public string? CatchCertificateReference { get; init; }

    [JsonPropertyName("issuedOn")]
    [Description("Catch certificate date of issue")]
    public DateOnly? IssuedOn { get; init; }

    [JsonPropertyName("flagState")]
    [Description("Catch certificate flag state of catching vessel(s)")]
    public string? FlagState { get; init; }

    [JsonPropertyName("species")]
    [Description("List of species imported under this catch certificate")]
    public List<string>? Species { get; init; }
}
