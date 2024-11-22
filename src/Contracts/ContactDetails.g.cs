using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ContactDetails
{
    [JsonPropertyName("name")]
    [Description("Name of designated contact")]
    public string? Name { get; init; }

    [JsonPropertyName("telephone")]
    [Description("Telephone number of designated contact")]
    public string? Telephone { get; init; }

    [JsonPropertyName("email")]
    [Description("Email address of designated contact")]
    public string? Email { get; init; }

    [JsonPropertyName("agent")]
    [Description("Name of agent representing designated contact")]
    public string? Agent { get; init; }
}
