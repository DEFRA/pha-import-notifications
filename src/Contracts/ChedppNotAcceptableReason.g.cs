namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ChedppNotAcceptableReason
    {
        [JsonPropertyName("reason")]
        [Description("")]
        public int Reason { get; set; }

        [JsonPropertyName("commodityOrPackage")]
        [Description("")]
        public int CommodityOrPackage { get; set; }
    }
}
