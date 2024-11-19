namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class ControlAuthority
    {
        [JsonPropertyName("officialVeterinarian")]
        [Description("")]
        public OfficialVeterinarian OfficialVeterinarian { get; set; }

        [JsonPropertyName("customsReferenceNo")]
        [Description("Customs reference number")]
        public string CustomsReferenceNo { get; set; }

        [JsonPropertyName("containerResealed")]
        [Description("Were containers resealed?")]
        public bool ContainerResealed { get; set; }

        [JsonPropertyName("newSealNumber")]
        [Description("When the containers are resealed they need new seal numbers")]
        public string NewSealNumber { get; set; }

        [JsonPropertyName("iuuFishingReference")]
        [Description("Illegal, Unreported and Unregulated (IUU) fishing reference number")]
        public string IuuFishingReference { get; set; }

        [JsonPropertyName("iuuCheckRequired")]
        [Description("Was Illegal, Unreported and Unregulated (IUU) check required")]
        public bool IuuCheckRequired { get; set; }

        [JsonPropertyName("iuuOption")]
        [Description("")]
        public int IuuOption { get; set; }
    }
}
