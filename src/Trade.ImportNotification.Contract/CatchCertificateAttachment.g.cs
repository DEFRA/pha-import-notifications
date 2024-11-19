namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CatchCertificateAttachment
    {
        [JsonPropertyName("attachmentId")]
        [Description("The UUID of the uploaded catch certificate file in blob storage")]
        public string AttachmentId { get; set; }

        [JsonPropertyName("numberOfCatchCertificates")]
        [Description("The total number of catch certificates on the attachment")]
        public int NumberOfCatchCertificates { get; set; }

        [JsonPropertyName("catchCertificateDetails")]
        [Description("List of catch certificate details")]
        public Array CatchCertificateDetails { get; set; }
    }
}
