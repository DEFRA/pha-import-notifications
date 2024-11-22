using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class OfficialInspector
{
    [JsonPropertyName("firstName")]
    [Description("First name of inspector")]
    public string? FirstName { get; init; }

    [JsonPropertyName("lastName")]
    [Description("Last name of inspector")]
    public string? LastName { get; init; }

    [JsonPropertyName("email")]
    [Description("Email of inspector")]
    public string? Email { get; init; }

    [JsonPropertyName("phone")]
    [Description("Phone number of inspector")]
    public string? Phone { get; init; }

    [JsonPropertyName("fax")]
    [Description("Fax number of inspector")]
    public string? Fax { get; init; }

    [JsonPropertyName("address")]
    public required Address Address { get; init; }

    [JsonPropertyName("signed")]
    [Description("Date of sign")]
    public string? Signed { get; init; }
}
