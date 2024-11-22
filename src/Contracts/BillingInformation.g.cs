using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class BillingInformation
{
    [JsonPropertyName("isConfirmed")]
    [Description("Indicates whether user has confirmed their billing information")]
    public bool? IsConfirmed { get; init; }

    [JsonPropertyName("emailAddress")]
    [Description("Billing email address")]
    public string? EmailAddress { get; init; }

    [JsonPropertyName("phoneNumber")]
    [Description("Billing phone number")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("contactName")]
    [Description("Billing Contact Name")]
    public string? ContactName { get; init; }

    [JsonPropertyName("postalAddress")]
    public required PostalAddress PostalAddress { get; init; }
}
