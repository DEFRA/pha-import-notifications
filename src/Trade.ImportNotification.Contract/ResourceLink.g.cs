namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ResourceLink
    {
        [JsonPropertyName("self")]
        [Description("")]
        public string[] Self { get; set; }
    }
}
