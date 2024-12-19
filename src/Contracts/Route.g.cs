#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class Route
{
    [JsonPropertyName("transitingStates")]
    public List<string>? TransitingStates { get; init; }
}
