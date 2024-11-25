#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class NotificationTdmRelationships
{
    [JsonPropertyName("movements")]
    public TdmRelationshipObject Movements { get; set; }
}
