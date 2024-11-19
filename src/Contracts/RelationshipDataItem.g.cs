namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class RelationshipDataItem
    {
        [JsonPropertyName("matched")]
        [Description("")]
        public bool Matched { get; set; }

        [JsonPropertyName("type")]
        [Description("")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        [Description("")]
        public string Id { get; set; }

        [JsonPropertyName("links")]
        [Description("")]
        public ResourceLink Links { get; set; }

        [JsonPropertyName("sourceItem")]
        [Description("")]
        public int SourceItem { get; set; }

        [JsonPropertyName("destinationItem")]
        [Description("")]
        public int DestinationItem { get; set; }

        [JsonPropertyName("matchingLevel")]
        [Description("")]
        public int MatchingLevel { get; set; }
    }
}
