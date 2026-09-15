using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class LocalDevTokenOptions
{
    public bool Enabled { get; init; }

    public string Issuer { get; init; } = "https://cognito-idp.eu-west-2.amazonaws.com/eu-west-2_LOCAL";

    [MinLength(32)]
    public string SigningKey { get; init; } = "pha-import-notifications-local-dev";

    [Range(1, 1440)]
    public int LifetimeMinutes { get; init; } = 60;
}
