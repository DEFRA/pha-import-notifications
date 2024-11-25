#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ControlAuthority
{
    [JsonPropertyName("officialVeterinarian")]
    public OfficialVeterinarian OfficialVeterinarian { get; set; }

    [JsonPropertyName("customsReferenceNo")]
    [Description("Customs reference number")]
    public string? CustomsReferenceNo { get; set; }

    [JsonPropertyName("containerResealed")]
    [Description("Were containers resealed?")]
    public bool? ContainerResealed { get; set; }

    [JsonPropertyName("newSealNumber")]
    [Description("When the containers are resealed they need new seal numbers")]
    public string? NewSealNumber { get; set; }

    [JsonPropertyName("iuuFishingReference")]
    [Description("Illegal, Unreported and Unregulated (IUU) fishing reference number")]
    public string? IuuFishingReference { get; set; }

    [JsonPropertyName("iuuCheckRequired")]
    [Description("Was Illegal, Unreported and Unregulated (IUU) check required")]
    public bool? IuuCheckRequired { get; set; }

    [JsonPropertyName("iuuOption")]
    public ControlAuthorityIuuOptionEnum IuuOption { get; set; }
}
