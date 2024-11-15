namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class CatchCertificates
    {
        [JsonPropertyName("certificateNumber")]
        [Description("The catch certificate number")]
        public string[] CertificateNumber { get; set; }

        [JsonPropertyName("weight")]
        [Description("The catch certificate weight number")]
        public decimal Weight { get; set; }
    }
}