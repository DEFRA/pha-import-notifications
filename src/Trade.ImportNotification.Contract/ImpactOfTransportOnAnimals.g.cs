namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ImpactOfTransportOnAnimals
    {
        [JsonPropertyName("numberOfDeadAnimals")]
        [Description("Number of dead animals specified by units")]
        public int NumberOfDeadAnimals { get; set; }

        [JsonPropertyName("numberOfDeadAnimalsUnit")]
        [Description("Unit used for specifying number of dead animals (percent or units)")]
        public string NumberOfDeadAnimalsUnit { get; set; }

        [JsonPropertyName("numberOfUnfitAnimals")]
        [Description("Number of unfit animals")]
        public int NumberOfUnfitAnimals { get; set; }

        [JsonPropertyName("numberOfUnfitAnimalsUnit")]
        [Description("Unit used for specifying number of unfit animals (percent or units)")]
        public string NumberOfUnfitAnimalsUnit { get; set; }

        [JsonPropertyName("numberOfBirthOrAbortion")]
        [Description("Number of births or abortions (unit)")]
        public int NumberOfBirthOrAbortion { get; set; }
    }
}
