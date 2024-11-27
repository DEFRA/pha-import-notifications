#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Address
{
    [JsonPropertyName("street")]
    [Description("Street")]
    public string? Street { get; init; }

    [JsonPropertyName("city")]
    [Description("City")]
    public string? City { get; init; }

    [JsonPropertyName("country")]
    [Description("Country")]
    public string? Country { get; init; }

    [JsonPropertyName("postalCode")]
    [Description("Postal Code")]
    public string? PostalCode { get; init; }

    [JsonPropertyName("addressLine1")]
    [Description("1st line of address")]
    public string? AddressLine1 { get; init; }

    [JsonPropertyName("addressLine2")]
    [Description("2nd line of address")]
    public string? AddressLine2 { get; init; }

    [JsonPropertyName("addressLine3")]
    [Description("3rd line of address")]
    public string? AddressLine3 { get; init; }

    [JsonPropertyName("postalZipCode")]
    [Description("Post / zip code")]
    public string? PostalZipCode { get; init; }

    [JsonPropertyName("countryIsoCode")]
    [Description("country 2-digits ISO code")]
    public string? CountryIsoCode { get; init; }

    [JsonPropertyName("email")]
    [Description("Email address")]
    public string? Email { get; init; }

    [JsonPropertyName("ukTelephone")]
    [Description("UK phone number")]
    public string? UkTelephone { get; init; }

    [JsonPropertyName("telephone")]
    [Description("Telephone number")]
    public string? Telephone { get; init; }

    [JsonPropertyName("internationalTelephone")]
    public required InternationalTelephone InternationalTelephone { get; init; }
}
