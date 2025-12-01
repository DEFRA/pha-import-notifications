#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportPreNotificationUpdateResponse
{
    [JsonPropertyName("referenceNumber")]
    public required string ReferenceNumber { get; init; }

    [JsonPropertyName("updated")]
    public required DateTime Updated { get; init; }
}
