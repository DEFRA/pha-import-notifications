namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class AccompanyingDocument
    {
        [JsonPropertyName("documentType")]
        [Description("")]
        public int DocumentType { get; set; }

        [JsonPropertyName("documentReference")]
        [Description("Additional document reference")]
        public string[] DocumentReference { get; set; }

        [JsonPropertyName("documentIssuedOn")]
        [Description("Additional document issue date")]
        public string[] DocumentIssuedOn { get; set; }

        [JsonPropertyName("attachmentId")]
        [Description("The UUID used for the uploaded file in blob storage")]
        public string[] AttachmentId { get; set; }

        [JsonPropertyName("attachmentFilename")]
        [Description("The original filename of the uploaded file")]
        public string[] AttachmentFilename { get; set; }

        [JsonPropertyName("attachmentContentType")]
        [Description("The MIME type of the uploaded file")]
        public string[] AttachmentContentType { get; set; }

        [JsonPropertyName("uploadUserId")]
        [Description("The UUID for the user that uploaded the file")]
        public string[] UploadUserId { get; set; }

        [JsonPropertyName("uploadOrganisationId")]
        [Description("The UUID for the organisation that the upload user is associated with")]
        public string[] UploadOrganisationId { get; set; }

        [JsonPropertyName("externalReference")]
        [Description("")]
        public ExternalReference ExternalReference { get; set; }
    }
}