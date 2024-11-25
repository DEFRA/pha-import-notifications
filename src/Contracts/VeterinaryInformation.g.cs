#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class VeterinaryInformation
{
    [JsonPropertyName("establishmentsOfOriginExternalReference")]
    public ExternalReference EstablishmentsOfOriginExternalReference { get; set; }

    [JsonPropertyName("establishmentsOfOrigins")]
    [Description("List of establishments which were approved by UK to issue veterinary documents")]
    public List<ApprovedEstablishment>? EstablishmentsOfOrigins { get; set; }

    [JsonPropertyName("veterinaryDocument")]
    [Description("Veterinary document identification")]
    public string? VeterinaryDocument { get; set; }

    [JsonPropertyName("veterinaryDocumentIssuedOn")]
    [Description("Veterinary document issue date")]
    public string? VeterinaryDocumentIssuedOn { get; set; }

    [JsonPropertyName("accompanyingDocumentNumbers")]
    [Description("Additional documents")]
    public List<string>? AccompanyingDocumentNumbers { get; set; }

    [JsonPropertyName("accompanyingDocuments")]
    [Description("Accompanying documents")]
    public List<AccompanyingDocument>? AccompanyingDocuments { get; set; }

    [JsonPropertyName("catchCertificateAttachments")]
    [Description("Catch certificate attachments")]
    public List<CatchCertificateAttachment>? CatchCertificateAttachments { get; set; }

    [JsonPropertyName("identificationDetails")]
    [Description("Details helpful for identification")]
    public List<IdentificationDetails>? IdentificationDetails { get; set; }
}
