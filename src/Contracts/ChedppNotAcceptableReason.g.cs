namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ChedppNotAcceptableReason
    {
        [JsonPropertyName("reason")]
        public ChedppNotAcceptableReasonReasonEnum Reason { get; init; }

        [JsonPropertyName("commodityOrPackage")]
        public ChedppNotAcceptableReasonCommodityOrPackageEnum CommodityOrPackage { get; init; }
    }
}
