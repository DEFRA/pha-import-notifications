#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record RelatedImportDeclarationsResponse
{
    [JsonPropertyName("customsDeclarations")]
    public required List<CustomsDeclarationResponse> CustomsDeclarations { get; init; }

    [JsonPropertyName("importPreNotifications")]
    public required List<ImportPreNotificationResponse> ImportPreNotifications { get; init; }
}
