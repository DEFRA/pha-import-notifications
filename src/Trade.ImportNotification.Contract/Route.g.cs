namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Route
    {
        [JsonPropertyName("transitingStates")]
        [Description("")]
        public Array TransitingStates { get; set; }
    }
}
