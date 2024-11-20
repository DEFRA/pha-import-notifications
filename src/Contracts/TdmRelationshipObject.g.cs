namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class TdmRelationshipObject
    {
        [JsonPropertyName("matched")]
        public bool Matched { get; init; }

        [JsonPropertyName("links")]
        public RelationshipLinks Links { get; init; }

        [JsonPropertyName("data")]
        public Array Data { get; init; }
    }
}
