#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ChedppNotAcceptableReason
{
    [JsonPropertyName("reason")]
    [ExampleValue("DocPhmdm")]
    [ExampleValue("DocPhmdii")]
    [ExampleValue("DocPa")]
    [ExampleValue("DocPic")]
    [ExampleValue("DocPill")]
    [ExampleValue("DocPed")]
    [ExampleValue("DocPmod")]
    [ExampleValue("DocPfi")]
    [ExampleValue("DocPnol")]
    [ExampleValue("DocPcne")]
    [ExampleValue("DocPadm")]
    [ExampleValue("DocPadi")]
    [ExampleValue("DocPpni")]
    [ExampleValue("DocPf")]
    [ExampleValue("DocPo")]
    [ExampleValue("DocNcevd")]
    [ExampleValue("DocNcpqefi")]
    [ExampleValue("DocNcpqebec")]
    [ExampleValue("DocNcts")]
    [ExampleValue("DocNco")]
    [ExampleValue("DocOrii")]
    [ExampleValue("DocOrsr")]
    [ExampleValue("OriOrrnu")]
    [ExampleValue("PhyOrpp")]
    [ExampleValue("PhyOrho")]
    [ExampleValue("PhyIs")]
    [ExampleValue("PhyOrsr")]
    [ExampleValue("OthCnl")]
    [ExampleValue("OthO")]
    [Description("reason for refusal")]
    public string? Reason { get; init; }

    [JsonPropertyName("commodityOrPackage")]
    [ExampleValue("C")]
    [ExampleValue("P")]
    [ExampleValue("Cp")]
    [Description("commodity or package")]
    public string? CommodityOrPackage { get; init; }
}
