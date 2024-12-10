namespace Defra.PhaImportNotifications.Api.Configuration;

public class BasicAuthOptions
{
    public bool Enabled { get; init; } = true;

    public string Password { get; init; } = string.Empty;

    public string Username { get; init; } = string.Empty;
}
