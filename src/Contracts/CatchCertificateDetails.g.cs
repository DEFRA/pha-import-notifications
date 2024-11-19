namespace Defra.PhaImportNotifications.Contracts
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CatchCertificateDetails
    {
        [JsonPropertyName("catchCertificateId")]
        [Description("The UUID of the catch certificate")]
        public string CatchCertificateId { get; set; }

        [JsonPropertyName("catchCertificateReference")]
        [Description("Catch certificate reference")]
        public string CatchCertificateReference { get; set; }

        [JsonPropertyName("issuedOn")]
        [Description("Catch certificate date of issue")]
        public string IssuedOn { get; set; }

        [JsonPropertyName("flagState")]
        [Description("Catch certificate flag state of catching vessel(s)")]
        public string FlagState { get; set; }

        [JsonPropertyName("species")]
        [Description("List of species imported under this catch certificate")]
        public Array Species { get; set; }
    }
}
