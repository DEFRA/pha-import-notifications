namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CommonUserCharge
    {
        [JsonPropertyName("wasSentToTradeCharge")]
        [Description("Indicates whether the last applicable change was successfully send over the interface to Trade Charge")]
        public bool WasSentToTradeCharge { get; set; }
    }
}
