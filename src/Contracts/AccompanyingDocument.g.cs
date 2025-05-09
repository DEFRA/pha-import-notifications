#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record AccompanyingDocument
{
    [JsonPropertyName("documentType")]
    [ExampleValue("AirWaybill")]
    [ExampleValue("BillOfLading")]
    [ExampleValue("CargoManifest")]
    [ExampleValue("CatchCertificate")]
    [ExampleValue("CommercialDocument")]
    [ExampleValue("CommercialInvoice")]
    [ExampleValue("ConformityCertificate")]
    [ExampleValue("ContainerManifest")]
    [ExampleValue("CustomsDeclaration")]
    [ExampleValue("Docom")]
    [ExampleValue("HealthCertificate")]
    [ExampleValue("HeatTreatmentCertificate")]
    [ExampleValue("ImportPermit")]
    [ExampleValue("InspectionCertificate")]
    [ExampleValue("Itahc")]
    [ExampleValue("JourneyLog")]
    [ExampleValue("LaboratorySamplingResultsForAflatoxin")]
    [ExampleValue("LatestVeterinaryHealthCertificate")]
    [ExampleValue("LetterOfAuthority")]
    [ExampleValue("LicenseOrAuthorisation")]
    [ExampleValue("MycotoxinCertification")]
    [ExampleValue("OriginCertificate")]
    [ExampleValue("Other")]
    [ExampleValue("PhytosanitaryCertificate")]
    [ExampleValue("ProcessingStatement")]
    [ExampleValue("ProofOfStorage")]
    [ExampleValue("RailwayBill")]
    [ExampleValue("SeaWaybill")]
    [ExampleValue("VeterinaryHealthCertificate")]
    [ExampleValue("ListOfIngredients")]
    [ExampleValue("PackingList")]
    [ExampleValue("RoadConsignmentNote")]
    [Description("Additional document type")]
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
