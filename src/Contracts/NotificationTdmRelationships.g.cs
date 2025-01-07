#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record NotificationTdmRelationships
{
    [JsonPropertyName("movements")]
    public TdmRelationshipObject? Movements { get; init; }
}
