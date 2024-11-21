namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Route
    {
        [JsonPropertyName("transitingStates")]
        public List<string> TransitingStates { get; init; }
    }
}
