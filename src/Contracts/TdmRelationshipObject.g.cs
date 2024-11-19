namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class TdmRelationshipObject
    {
        [JsonPropertyName("matched")]
        public bool Matched { get; set; }

        [JsonPropertyName("links")]
        public RelationshipLinks Links { get; set; }

        [JsonPropertyName("data")]
        public Array Data { get; set; }
    }
}
