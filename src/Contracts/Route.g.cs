using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Route
{
    [JsonPropertyName("transitingStates")]
    public List<string>? TransitingStates { get; init; }
}
