#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record Gmr
{
    [JsonPropertyName("id")]
    [Description("The Goods Movement Record (GMR) ID for this GMR.  Do not include when POSTing a GMR - GVMS will assign an ID.")]
    public string? Id { get; init; }

    [JsonPropertyName("haulierEori")]
    [JsonIgnore]
    [Description("The EORI of the haulier that is responsible for the vehicle making the goods movement")]
    public string? HaulierEori { get; init; }

    [JsonPropertyName("state")]
    [ExampleValue("NOT_FINALISABLE")]
    [ExampleValue("OPEN")]
    [ExampleValue("CHECKED_IN")]
    [ExampleValue("EMBARKED")]
    [ExampleValue("COMPLETED")]
    [Description("The state of the GMR. Possible values taken from GVMS schema version v1.0 (private beta).")]
    public string? State { get; init; }

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
    [ExampleValue("UK_INBOUND")]
    [ExampleValue("UK_OUTBOUND")]
    [ExampleValue("GB_TO_NI")]
    [ExampleValue("NI_TO_GB")]
    [JsonIgnore]
    [Description("The direction of the movement - into or out of the UK, or between Great Britain and Northern Ireland. Possible values taken from GVMS schema version v1.0 (private beta).")]
    public string? Direction { get; init; }

    [JsonPropertyName("haulierType")]
    [ExampleValue("STANDARD")]
    [ExampleValue("FPO_ASN")]
    [ExampleValue("FPO_OTHER")]
    [ExampleValue("NATO_MOD")]
    [ExampleValue("RMG")]
    [ExampleValue("ETOE")]
    [JsonIgnore]
    [Description("The type of haulier moving the goods. Possible values taken from GVMS schema version v1.0 (private beta).")]
    public string? HaulierType { get; init; }

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

    [JsonPropertyName("declarations")]
    public Declarations? Declarations { get; init; }
}
