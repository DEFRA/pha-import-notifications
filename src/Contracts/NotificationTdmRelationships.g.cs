namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NotificationTdmRelationships
    {
        [JsonPropertyName("movements")]
        public TdmRelationshipObject Movements { get; init; }
    }
}
