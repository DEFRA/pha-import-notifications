namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class Identifiers
    {
        [JsonPropertyName("speciesNumber")]
        [Description("Number used to identify which item the identifiers are related to")]
        public int SpeciesNumber { get; init; }

        [JsonPropertyName("data")]
        [Description("List of identifiers and their keys")]
        public object Data { get; init; }

        [JsonPropertyName("isPlaceOfDestinationThePermanentAddress")]
        [Description("Is the place of destination the permanent address?")]
        public bool IsPlaceOfDestinationThePermanentAddress { get; init; }

        [JsonPropertyName("permanentAddress")]
        public EconomicOperator PermanentAddress { get; init; }
    }
}