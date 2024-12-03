#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class PartThree
{
    [JsonPropertyName("controlStatus")]
    [Description("Control status enum")]
    public PartThreeControlStatusEnum? ControlStatus { get; init; }

    [JsonPropertyName("control")]
    [Description("Control details")]
    public Control? Control { get; init; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for Part 3 - Control")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; init; }

    [JsonPropertyName("sealCheckRequired")]
    [Description("Is the seal check required")]
    public bool? SealCheckRequired { get; init; }

    [JsonPropertyName("sealCheck")]
    [Description("Seal check details")]
    public SealCheck? SealCheck { get; init; }

    [JsonPropertyName("sealCheckOverride")]
    [Description("Seal check override details")]
    public InspectionOverride? SealCheckOverride { get; init; }
}
