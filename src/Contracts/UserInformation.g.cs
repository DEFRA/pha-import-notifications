#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class UserInformation
{
    [JsonPropertyName("displayName")]
    [Description("Display name")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("userId")]
    [Description("User ID")]
    public string? UserId { get; init; }

    [JsonPropertyName("isControlUser")]
    [Description("Is this user a control")]
    public bool? IsControlUser { get; init; }
}
