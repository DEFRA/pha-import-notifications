namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class VeterinaryInformation
    {
        [JsonPropertyName("establishmentsOfOriginExternalReference")]
        [Description("")]
        public ExternalReference EstablishmentsOfOriginExternalReference { get; set; }

        [JsonPropertyName("establishmentsOfOrigins")]
        [Description("List of establishments which were approved by UK to issue veterinary documents")]
        public Array EstablishmentsOfOrigins { get; set; }

        [JsonPropertyName("veterinaryDocument")]
        [Description("Veterinary document identification")]
        public string VeterinaryDocument { get; set; }

        [JsonPropertyName("veterinaryDocumentIssuedOn")]
        [Description("Veterinary document issue date")]
        public string VeterinaryDocumentIssuedOn { get; set; }

        [JsonPropertyName("accompanyingDocumentNumbers")]
        [Description("Additional documents")]
        public Array AccompanyingDocumentNumbers { get; set; }

        [JsonPropertyName("accompanyingDocuments")]
        [Description("Accompanying documents")]
        public Array AccompanyingDocuments { get; set; }

        [JsonPropertyName("catchCertificateAttachments")]
        [Description("Catch certificate attachments")]
        public Array CatchCertificateAttachments { get; set; }

        [JsonPropertyName("identificationDetails")]
        [Description("Details helpful for identification")]
        public Array IdentificationDetails { get; set; }
    }
}
