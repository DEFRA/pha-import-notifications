namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Identifiers
    {
        [JsonPropertyName("speciesNumber")]
        [Description("Number used to identify which item the identifiers are related to")]
        public int SpeciesNumber { get; set; }

        [JsonPropertyName("data")]
        [Description("List of identifiers and their keys")]
        public object Data { get; set; }

        [JsonPropertyName("isPlaceOfDestinationThePermanentAddress")]
        [Description("Is the place of destination the permanent address?")]
        public bool IsPlaceOfDestinationThePermanentAddress { get; set; }

        [JsonPropertyName("permanentAddress")]
        [Description("")]
        public EconomicOperator PermanentAddress { get; set; }
    }
}
