#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Address
{
    [JsonPropertyName("street")]
    [Description("Street")]
    public string? Street { get; set; }

    [JsonPropertyName("city")]
    [Description("City")]
    public string? City { get; set; }

    [JsonPropertyName("country")]
    [Description("Country")]
    public string? Country { get; set; }

    [JsonPropertyName("postalCode")]
    [Description("Postal Code")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("addressLine1")]
    [Description("1st line of address")]
    public string? AddressLine1 { get; set; }

    [JsonPropertyName("addressLine2")]
    [Description("2nd line of address")]
    public string? AddressLine2 { get; set; }

    [JsonPropertyName("addressLine3")]
    [Description("3rd line of address")]
    public string? AddressLine3 { get; set; }

    [JsonPropertyName("postalZipCode")]
    [Description("Post / zip code")]
    public string? PostalZipCode { get; set; }

    [JsonPropertyName("countryIsoCode")]
    [Description("country 2-digits ISO code")]
    public string? CountryIsoCode { get; set; }

    [JsonPropertyName("email")]
    [Description("Email address")]
    public string? Email { get; set; }

    [JsonPropertyName("ukTelephone")]
    [Description("UK phone number")]
    public string? UkTelephone { get; set; }

    [JsonPropertyName("telephone")]
    [Description("Telephone number")]
    public string? Telephone { get; set; }

    [JsonPropertyName("internationalTelephone")]
    public InternationalTelephone InternationalTelephone { get; set; }
}
