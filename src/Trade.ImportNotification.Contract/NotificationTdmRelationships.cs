namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class NotificationTdmRelationships
    {
        [JsonPropertyName("movements")]
        [Description("")]
        public TdmRelationshipObject Movements { get; set; }
    }
}