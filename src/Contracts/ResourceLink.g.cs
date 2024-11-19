namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ResourceLink
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }
}
