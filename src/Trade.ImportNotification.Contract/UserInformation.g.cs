namespace Trade.ImportNotification.Contract
{
    using System.Text.Json.Serialization;
    using System.ComponentModel;

    public class UserInformation
    {
        [JsonPropertyName("displayName")]
        [Description("Display name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("userId")]
        [Description("User ID")]
        public string UserId { get; set; }

        [JsonPropertyName("isControlUser")]
        [Description("Is this user a control")]
        public bool IsControlUser { get; set; }
    }
}
