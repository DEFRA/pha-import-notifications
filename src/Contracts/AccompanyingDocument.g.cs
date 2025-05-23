#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record AccompanyingDocument
{
    [JsonPropertyName("documentType")]
    [ExampleValue("airWaybill")]
    [ExampleValue("billOfLading")]
    [ExampleValue("cargoManifest")]
    [ExampleValue("catchCertificate")]
    [ExampleValue("commercialDocument")]
    [ExampleValue("commercialInvoice")]
    [ExampleValue("conformityCertificate")]
    [ExampleValue("containerManifest")]
    [ExampleValue("customsDeclaration")]
    [ExampleValue("docom")]
    [ExampleValue("healthCertificate")]
    [ExampleValue("heatTreatmentCertificate")]
    [ExampleValue("importPermit")]
    [ExampleValue("inspectionCertificate")]
    [ExampleValue("itahc")]
    [ExampleValue("journeyLog")]
    [ExampleValue("laboratorySamplingResultsForAflatoxin")]
    [ExampleValue("latestVeterinaryHealthCertificate")]
    [ExampleValue("letterOfAuthority")]
    [ExampleValue("licenseOrAuthorisation")]
    [ExampleValue("mycotoxinCertification")]
    [ExampleValue("originCertificate")]
    [ExampleValue("other")]
    [ExampleValue("phytosanitaryCertificate")]
    [ExampleValue("processingStatement")]
    [ExampleValue("proofOfStorage")]
    [ExampleValue("railwayBill")]
    [ExampleValue("seaWaybill")]
    [ExampleValue("veterinaryHealthCertificate")]
    [ExampleValue("listOfIngredients")]
    [ExampleValue("packingList")]
    [ExampleValue("roadConsignmentNote")]
    [Description("Additional document type. Possible values taken from IPAFFS schema version 17.5.")]
    public string? DocumentType { get; init; }

    [JsonPropertyName("documentReference")]
    [Description("Additional document reference")]
    public string? DocumentReference { get; init; }

    [JsonPropertyName("documentIssuedOn")]
    [Description("Additional document issue date")]
    public DateOnly? DocumentIssuedOn { get; init; }

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
    [Description("External reference of accompanying document, which relates to a downstream service")]
    public ExternalReference? ExternalReference { get; init; }
}
