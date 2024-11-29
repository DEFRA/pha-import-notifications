#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class VeterinaryInformation
{
    [JsonPropertyName("establishmentsOfOriginExternalReference")]
    public required ExternalReference EstablishmentsOfOriginExternalReference { get; init; }

    [JsonPropertyName("establishmentsOfOrigins")]
    [Description("List of establishments which were approved by UK to issue veterinary documents")]
    public List<ApprovedEstablishment>? EstablishmentsOfOrigins { get; init; }

    [JsonPropertyName("veterinaryDocument")]
    [Description("Veterinary document identification")]
    public string? VeterinaryDocument { get; init; }

    [JsonPropertyName("veterinaryDocumentIssueDate")]
    [Description("Veterinary document issue date")]
    public string? VeterinaryDocumentIssueDate { get; init; }

    [JsonPropertyName("accompanyingDocumentNumbers")]
    [Description("Additional documents")]
    public List<string>? AccompanyingDocumentNumbers { get; init; }

    [JsonPropertyName("accompanyingDocuments")]
    [Description("Accompanying documents")]
    public List<AccompanyingDocument>? AccompanyingDocuments { get; init; }

    [JsonPropertyName("catchCertificateAttachments")]
    [Description("Catch certificate attachments")]
    public List<CatchCertificateAttachment>? CatchCertificateAttachments { get; init; }

    [JsonPropertyName("identificationDetails")]
    [Description("Details helpful for identification")]
    public List<IdentificationDetails>? IdentificationDetails { get; init; }
}
