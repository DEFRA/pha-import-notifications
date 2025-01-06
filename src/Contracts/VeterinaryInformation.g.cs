#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record VeterinaryInformation
{
    [JsonPropertyName("establishmentsOfOriginExternalReference")]
    [Description("External reference of approved establishments, which relates to a downstream service")]
    public ExternalReference? EstablishmentsOfOriginExternalReference { get; init; }

    [JsonPropertyName("establishmentsOfOrigins")]
    [Description("List of establishments which were approved by UK to issue veterinary documents")]
    public List<ApprovedEstablishment>? EstablishmentsOfOrigins { get; init; }

    [JsonPropertyName("veterinaryDocument")]
    [Description("Veterinary document identification")]
    public string? VeterinaryDocument { get; init; }

    [JsonPropertyName("veterinaryDocumentIssuedOn")]
    [Description("Veterinary document issue date")]
    public string? VeterinaryDocumentIssuedOn { get; init; }

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
    [JsonIgnore]
    [Description("Details helpful for identification")]
    public List<IdentificationDetails>? IdentificationDetails { get; init; }
}
