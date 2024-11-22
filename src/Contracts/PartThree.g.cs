using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class PartThree
{
    [JsonPropertyName("controlStatus")]
    public required PartThreeControlStatusEnum ControlStatus { get; init; }

    [JsonPropertyName("control")]
    public required Control Control { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for Part 3 - Control")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("sealCheckRequired")]
    [Description("Is the seal check required")]
    public bool? SealCheckRequired { get; init; }

    [JsonPropertyName("sealCheck")]
    public required SealCheck SealCheck { get; init; }

    [JsonPropertyName("sealCheckOverride")]
    public required InspectionOverride SealCheckOverride { get; init; }
}
