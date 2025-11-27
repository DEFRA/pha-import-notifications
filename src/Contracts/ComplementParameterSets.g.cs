#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ComplementParameterSets
{
    [JsonPropertyName("uniqueComplementId")]
    [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
    public string? UniqueComplementId { get; init; }

    [JsonPropertyName("complementId")]
    public int? ComplementId { get; init; }

    [JsonPropertyName("speciesId")]
    public string? SpeciesId { get; init; }

    [JsonPropertyName("keyDataPair")]
    public List<KeyDataPair>? KeyDataPair { get; init; }

    [JsonPropertyName("catchCertificates")]
    [JsonIgnore]
    [Description("Catch certificate details")]
    public List<CatchCertificates>? CatchCertificates { get; init; }

    [JsonPropertyName("identifiers")]
    [Description("Data used to identify the complements inside an IMP consignment")]
    public List<Identifiers>? Identifiers { get; init; }
}
