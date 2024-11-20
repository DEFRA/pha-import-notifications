namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ChedppNotAcceptableReason
    {
        [JsonPropertyName("reason")]
        public int Reason { get; init; }

        [JsonPropertyName("commodityOrPackage")]
        public int CommodityOrPackage { get; init; }
    }
}
