namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class TdmRelationshipObject
    {
        [JsonPropertyName("matched")]
        [Description("")]
        public bool Matched { get; set; }

        [JsonPropertyName("links")]
        [Description("")]
        public RelationshipLinks Links { get; set; }

        [JsonPropertyName("data")]
        [Description("")]
        public Array Data { get; set; }
    }
}