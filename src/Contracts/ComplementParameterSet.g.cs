namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ComplementParameterSet
    {
        [JsonPropertyName("uniqueComplementId")]
        [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
        public string UniqueComplementId { get; init; }

        [JsonPropertyName("complementId")]
        public int ComplementId { get; init; }

        [JsonPropertyName("speciesId")]
        public string SpeciesId { get; init; }

        [JsonPropertyName("keyDataPairs")]
        public object KeyDataPairs { get; init; }

        [JsonPropertyName("catchCertificates")]
        [Description("Catch certificate details")]
        public Array CatchCertificates { get; init; }

        [JsonPropertyName("identifiers")]
        [Description("Data used to identify the complements inside an IMP consignment")]
        public Array Identifiers { get; init; }
    }
}
