#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class ControlAuthority
{
    [JsonPropertyName("officialVeterinarian")]
    public required OfficialVeterinarian OfficialVeterinarian { get; init; }

    [JsonPropertyName("customsReferenceNo")]
    [Description("Customs reference number")]
    public string? CustomsReferenceNo { get; init; }

    [JsonPropertyName("containerResealed")]
    [Description("Were containers resealed?")]
    public bool? ContainerResealed { get; init; }

    [JsonPropertyName("newSealNumber")]
    [Description("When the containers are resealed they need new seal numbers")]
    public string? NewSealNumber { get; init; }

    [JsonPropertyName("iuuFishingReference")]
    [JsonIgnore]
    [Description("Illegal, Unreported and Unregulated (IUU) fishing reference number")]
    public string? IuuFishingReference { get; init; }

    [JsonPropertyName("iuuCheckRequired")]
    [Description("Was Illegal, Unreported and Unregulated (IUU) check required")]
    public bool? IuuCheckRequired { get; init; }

    [JsonPropertyName("iuuOption")]
    public required ControlAuthorityIuuOptionEnum IuuOption { get; init; }
}
