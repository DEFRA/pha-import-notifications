#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class CommonUserCharge
{
    [JsonPropertyName("wasSentToTradeCharge")]
    [Description("Indicates whether the last applicable change was successfully send over the interface to Trade Charge")]
    public bool? WasSentToTradeCharge { get; init; }
}
