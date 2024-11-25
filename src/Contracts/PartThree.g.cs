#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class PartThree
{
    [JsonPropertyName("controlStatus")]
    public PartThreeControlStatusEnum ControlStatus { get; set; }

    [JsonPropertyName("control")]
    public Control Control { get; set; }

    [JsonPropertyName("consignmentValidations")]
    [Description("Validation messages for Part 3 - Control")]
    public List<ValidationMessageCode>? ConsignmentValidations { get; set; }

    [JsonPropertyName("sealCheckRequired")]
    [Description("Is the seal check required")]
    public bool? SealCheckRequired { get; set; }

    [JsonPropertyName("sealCheck")]
    public SealCheck SealCheck { get; set; }

    [JsonPropertyName("sealCheckOverride")]
    public InspectionOverride SealCheckOverride { get; set; }
}
