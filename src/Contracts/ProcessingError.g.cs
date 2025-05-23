#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ProcessingError
{
    [JsonPropertyName("notifications")]
    public List<ErrorNotification>? Notifications { get; init; }
}
