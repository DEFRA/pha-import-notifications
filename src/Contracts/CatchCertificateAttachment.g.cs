#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class CatchCertificateAttachment
{
    [JsonPropertyName("attachmentId")]
    [Description("The UUID of the uploaded catch certificate file in blob storage")]
    public string? AttachmentId { get; init; }

    [JsonPropertyName("numberOfCatchCertificates")]
    [Description("The total number of catch certificates on the attachment")]
    public int? NumberOfCatchCertificates { get; init; }

    [JsonPropertyName("catchCertificateDetails")]
    [Description("List of catch certificate details")]
    public List<CatchCertificateDetails>? CatchCertificateDetails { get; init; }
}
