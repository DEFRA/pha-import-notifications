#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class ImpactOfTransportOnAnimals
{
    [JsonPropertyName("numberOfDeadAnimals")]
    [Description("Number of dead animals specified by units")]
    public int? NumberOfDeadAnimals { get; init; }

    [JsonPropertyName("numberOfDeadAnimalsUnit")]
    [Description("Unit used for specifying number of dead animals (percent or units)")]
    public string? NumberOfDeadAnimalsUnit { get; init; }

    [JsonPropertyName("numberOfUnfitAnimals")]
    [Description("Number of unfit animals")]
    public int? NumberOfUnfitAnimals { get; init; }

    [JsonPropertyName("numberOfUnfitAnimalsUnit")]
    [Description("Unit used for specifying number of unfit animals (percent or units)")]
    public string? NumberOfUnfitAnimalsUnit { get; init; }

    [JsonPropertyName("numberOfBirthOrAbortion")]
    [Description("Number of births or abortions (unit)")]
    public int? NumberOfBirthOrAbortion { get; init; }
}
