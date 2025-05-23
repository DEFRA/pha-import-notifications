#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ChedppNotAcceptableReason
{
    [JsonPropertyName("reason")]
    [ExampleValue("doc-phmdm")]
    [ExampleValue("doc-phmdii")]
    [ExampleValue("doc-pa")]
    [ExampleValue("doc-pic")]
    [ExampleValue("doc-pill")]
    [ExampleValue("doc-ped")]
    [ExampleValue("doc-pmod")]
    [ExampleValue("doc-pfi")]
    [ExampleValue("doc-pnol")]
    [ExampleValue("doc-pcne")]
    [ExampleValue("doc-padm")]
    [ExampleValue("doc-padi")]
    [ExampleValue("doc-ppni")]
    [ExampleValue("doc-pf")]
    [ExampleValue("doc-po")]
    [ExampleValue("doc-ncevd")]
    [ExampleValue("doc-ncpqefi")]
    [ExampleValue("doc-ncpqebec")]
    [ExampleValue("doc-ncts")]
    [ExampleValue("doc-nco")]
    [ExampleValue("doc-orii")]
    [ExampleValue("doc-orsr")]
    [ExampleValue("ori-orrnu")]
    [ExampleValue("phy-orpp")]
    [ExampleValue("phy-orho")]
    [ExampleValue("phy-is")]
    [ExampleValue("phy-orsr")]
    [ExampleValue("oth-cnl")]
    [ExampleValue("oth-o")]
    [Description("reason for refusal. Possible values taken from IPAFFS schema version 17.5.")]
    public string? Reason { get; init; }

    [JsonPropertyName("commodityOrPackage")]
    [ExampleValue("c")]
    [ExampleValue("p")]
    [ExampleValue("cp")]
    [Description("commodity or package. Possible values taken from IPAFFS schema version 17.5.")]
    public string? CommodityOrPackage { get; init; }
}
