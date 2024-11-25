#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class InternationalTelephone
{
    [JsonPropertyName("countryCode")]
    [Description("Country code of phone number")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("subscriberNumber")]
    [Description("Phone number")]
    public string? SubscriberNumber { get; set; }
}
