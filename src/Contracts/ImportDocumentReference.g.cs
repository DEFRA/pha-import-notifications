#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ImportDocumentReference
{
    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
