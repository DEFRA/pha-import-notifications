#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class ChedppNotAcceptableReason
{
    [JsonPropertyName("reason")]
    [Description("reason for refusal")]
    public ChedppNotAcceptableReasonReasonEnum? Reason { get; init; }

    [JsonPropertyName("commodityOrPackage")]
    [Description("commodity or package")]
    public ChedppNotAcceptableReasonCommodityOrPackageEnum? CommodityOrPackage { get; init; }
}
