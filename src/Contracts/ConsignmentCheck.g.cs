#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ConsignmentCheck
{
    [JsonPropertyName("euStandard")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Does it conform EU standards. Possible values taken from IPAFFS schema version 17.5.")]
    public string? EuStandard { get; init; }

    [JsonPropertyName("additionalGuarantees")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of additional guarantees. Possible values taken from IPAFFS schema version 17.5.")]
    public string? AdditionalGuarantees { get; init; }

    [JsonPropertyName("documentCheckAdditionalDetails")]
    [Description("Additional details for document check")]
    public string? DocumentCheckAdditionalDetails { get; init; }

    [JsonPropertyName("documentCheckResult")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of document check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? DocumentCheckResult { get; init; }

    [JsonPropertyName("nationalRequirements")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of national requirements check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? NationalRequirements { get; init; }

    [JsonPropertyName("identityCheckDone")]
    [Description("Was identity check done")]
    public bool? IdentityCheckDone { get; init; }

    [JsonPropertyName("identityCheckType")]
    [ExampleValue("Seal Check")]
    [ExampleValue("Full Identity Check")]
    [ExampleValue("Not Done")]
    [Description("Type of identity check performed. Possible values taken from IPAFFS schema version 17.5.")]
    public string? IdentityCheckType { get; init; }

    [JsonPropertyName("identityCheckResult")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of identity check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? IdentityCheckResult { get; init; }

    [JsonPropertyName("identityCheckNotDoneReason")]
    [ExampleValue("Reduced checks regime")]
    [ExampleValue("Not required")]
    [ExampleValue("Chilled equine semen facilitation scheme")]
    [Description("What was the reason for skipping identity check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? IdentityCheckNotDoneReason { get; init; }

    [JsonPropertyName("physicalCheckDone")]
    [Description("Was physical check done")]
    public bool? PhysicalCheckDone { get; init; }

    [JsonPropertyName("physicalCheckResult")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of physical check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? PhysicalCheckResult { get; init; }

    [JsonPropertyName("physicalCheckNotDoneReason")]
    [ExampleValue("Reduced checks regime")]
    [ExampleValue("Other")]
    [Description("What was the reason for skipping physical check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? PhysicalCheckNotDoneReason { get; init; }

    [JsonPropertyName("physicalCheckOtherText")]
    [Description("Other reason to not do physical check")]
    public string? PhysicalCheckOtherText { get; init; }

    [JsonPropertyName("welfareCheck")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Welfare check. Possible values taken from IPAFFS schema version 17.5.")]
    public string? WelfareCheck { get; init; }

    [JsonPropertyName("numberOfAnimalsChecked")]
    [Description("Number of animals checked")]
    public int? NumberOfAnimalsChecked { get; init; }

    [JsonPropertyName("laboratoryCheckDone")]
    [Description("Were laboratory tests done")]
    public bool? LaboratoryCheckDone { get; init; }

    [JsonPropertyName("laboratoryCheckResult")]
    [ExampleValue("Satisfactory")]
    [ExampleValue("Satisfactory following official intervention")]
    [ExampleValue("Not Satisfactory")]
    [ExampleValue("Not Done")]
    [ExampleValue("Derogation")]
    [ExampleValue("Not Set")]
    [Description("Result of laboratory tests. Possible values taken from IPAFFS schema version 17.5.")]
    public string? LaboratoryCheckResult { get; init; }
}
