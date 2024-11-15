namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class RelationshipLinks
    {
        [JsonPropertyName("self")]
        [Description("")]
        public string[] Self { get; set; }

        [JsonPropertyName("related")]
        [Description("")]
        public string[] Related { get; set; }
    }
}