#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ChedppNotAcceptableReason
{
    [JsonPropertyName("reason")]
    public ChedppNotAcceptableReasonReasonEnum Reason { get; set; }

    [JsonPropertyName("commodityOrPackage")]
    public ChedppNotAcceptableReasonCommodityOrPackageEnum CommodityOrPackage { get; set; }
}
