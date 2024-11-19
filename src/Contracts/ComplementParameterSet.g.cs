namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ComplementParameterSet
    {
        [JsonPropertyName("uniqueComplementId")]
        [Description("UUID used to match commodityComplement to its complementParameter set. CHEDPP only")]
        public string UniqueComplementId { get; set; }

        [JsonPropertyName("complementId")]
        public int ComplementId { get; set; }

        [JsonPropertyName("speciesId")]
        public string SpeciesId { get; set; }

        [JsonPropertyName("keyDataPairs")]
        public object KeyDataPairs { get; set; }

        [JsonPropertyName("catchCertificates")]
        [Description("Catch certificate details")]
        public Array CatchCertificates { get; set; }

        [JsonPropertyName("identifiers")]
        [Description("Data used to identify the complements inside an IMP consignment")]
        public Array Identifiers { get; set; }
    }
}
