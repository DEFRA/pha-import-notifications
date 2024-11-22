using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ChedppNotAcceptableReason
{
    [JsonPropertyName("reason")]
    public required ChedppNotAcceptableReasonReasonEnum Reason { get; init; }

    [JsonPropertyName("commodityOrPackage")]
    public required ChedppNotAcceptableReasonCommodityOrPackageEnum CommodityOrPackage { get; init; }
}
