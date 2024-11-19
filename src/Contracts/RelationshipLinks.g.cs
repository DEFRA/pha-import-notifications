namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class RelationshipLinks
    {
        [JsonPropertyName("self")]
        public string Self { get; init; }

        [JsonPropertyName("related")]
        public string Related { get; init; }
    }
}
