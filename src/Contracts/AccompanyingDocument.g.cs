#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class AccompanyingDocument
{
    [JsonPropertyName("documentType")]
    public required AccompanyingDocumentDocumentTypeEnum DocumentType { get; init; }

    [JsonPropertyName("documentReference")]
    [Description("Additional document reference")]
    public string? DocumentReference { get; init; }

    [JsonPropertyName("documentIssuedOn")]
    [Description("Additional document issue date")]
    public DateTime? DocumentIssuedOn { get; init; }

    [JsonPropertyName("attachmentId")]
    [Description("The UUID used for the uploaded file in blob storage")]
    public string? AttachmentId { get; init; }

    [JsonPropertyName("attachmentFilename")]
    [Description("The original filename of the uploaded file")]
    public string? AttachmentFilename { get; init; }

    [JsonPropertyName("attachmentContentType")]
    [Description("The MIME type of the uploaded file")]
    public string? AttachmentContentType { get; init; }

    [JsonPropertyName("uploadUserId")]
    [Description("The UUID for the user that uploaded the file")]
    public string? UploadUserId { get; init; }

    [JsonPropertyName("uploadOrganisationId")]
    [Description("The UUID for the organisation that the upload user is associated with")]
    public string? UploadOrganisationId { get; init; }

    [JsonPropertyName("externalReference")]
    public required ExternalReference ExternalReference { get; init; }
}
