#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record ComplementParameterSet
{
    [JsonPropertyName("uniqueComplementID")]
    [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
    public string? UniqueComplementID { get; init; }

    [JsonPropertyName("complementID")]
    public int? ComplementID { get; init; }

    [JsonPropertyName("speciesID")]
    public string? SpeciesID { get; init; }

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
