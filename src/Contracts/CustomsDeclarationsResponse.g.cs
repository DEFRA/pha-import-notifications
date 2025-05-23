#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsDeclarationsResponse
{
    [JsonPropertyName("customsDeclarations")]
    public required List<CustomsDeclarationResponse> CustomsDeclarations { get; init; }
}
