#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Gmr
{
    [JsonPropertyName("createdSource")]
    [JsonIgnore]
    public DateTime? CreatedSource { get; init; }

    [JsonPropertyName("created")]
    [JsonIgnore]
    public DateTime? Created { get; init; }

    [JsonPropertyName("updatedEntity")]
    [JsonIgnore]
    public DateTime? UpdatedEntity { get; init; }

    [JsonPropertyName("auditEntries")]
    [JsonIgnore]
    public List<AuditEntry>? AuditEntries { get; init; }

    [JsonPropertyName("relationships")]
    [JsonIgnore]
    public GmrRelationships? Relationships { get; init; }

    [JsonPropertyName("id")]
    [Description("The Goods Movement Record (GMR) ID for this GMR.  Do not include when POSTing a GMR - GVMS will assign an ID.")]
    public string? Id { get; init; }

    [JsonPropertyName("haulierEori")]
    [JsonIgnore]
    [Description("The EORI of the haulier that is responsible for the vehicle making the goods movement")]
    public string? HaulierEori { get; init; }

    [JsonPropertyName("state")]
    [Description("The state of the GMR")]
    public StateEnum? State { get; init; }

    [JsonPropertyName("inspectionRequired")]
    [JsonIgnore]
    [Description("If set to true, indicates that the vehicle requires a customs inspection.  If set to false, indicates that the vehicle does not require a customs inspection.  If not set, indicates the customs inspection decision has not yet been made or is not applicable.  For outbound GMRs, this indicates that the vehicle must present at an inspection facility prior to checking-in at the port.  For Office of Transit inspections for inbound GMRs, a decision may be made to inspect subsequent to a notification that inspection is not required.  In this event a notification will be sent that changes inspectionRequired from false to true.  If this happens after leaving the port of arrival, the inspection should be carried out at the next transit office e.g. the office of destination.")]
    public bool? InspectionRequired { get; init; }

    [JsonPropertyName("reportToLocations")]
    [JsonIgnore]
    [Description("A list of one or more inspection types, from GVMS Reference Data, that the vehicle must have carried out, in the order specified.  This means that where there are multiple entries here, the vehicle must report for the first inspection type listed before each subsequent listed inspection.  Each entry includes a list of available locations for the inspection type.")]
    public List<ReportToLocations>? ReportToLocations { get; init; }

    [JsonPropertyName("updatedSource")]
    [Description("The date and time that this GMR was last updated.")]
    public DateTime? UpdatedSource { get; init; }

    [JsonPropertyName("direction")]
    [JsonIgnore]
    [Description("The direction of the movement - into or out of the UK, or between Great Britain and Northern Ireland")]
    public DirectionEnum? Direction { get; init; }

    [JsonPropertyName("haulierType")]
    [JsonIgnore]
    [Description("The type of haulier moving the goods")]
    public HaulierTypeEnum? HaulierType { get; init; }

    [JsonPropertyName("isUnaccompanied")]
    [Description("Set to true if the vehicle will not be accompanying the trailer(s) during the crossing, or if the vehicle is carrying a container that will be detached and loaded for the crossing.")]
    public bool? IsUnaccompanied { get; init; }

    [JsonPropertyName("vehicleRegistrationNumber")]
    [Description("Vehicle registration number of the vehicle that will arrive at port.  If isUnaccompanied is set to false then vehicleRegNum must be provided to check-in.")]
    public string? VehicleRegistrationNumber { get; init; }

    [JsonPropertyName("trailerRegistrationNums")]
    [Description("For vehicles carrying trailers, the trailer registration number of each trailer.  If isUnaccompanied is set to true then trailerRegistrationNums or containerReferenceNums must be provided before check-in.")]
    public List<string>? TrailerRegistrationNums { get; init; }

    [JsonPropertyName("containerReferenceNums")]
    [Description("For vehicles arriving with containers that will be detached and loaded, the container reference number of each container in the movement. If isUnaccompanied is set to true then trailerRegistrationNums or containerReferenceNums must be provided before check-in.")]
    public List<string>? ContainerReferenceNums { get; init; }

    [JsonPropertyName("plannedCrossing")]
    public PlannedCrossing? PlannedCrossing { get; init; }

    [JsonPropertyName("checkedInCrossing")]
    [JsonIgnore]
    public CheckedInCrossing? CheckedInCrossing { get; init; }

    [JsonPropertyName("actualCrossing")]
    public ActualCrossing? ActualCrossing { get; init; }
}
