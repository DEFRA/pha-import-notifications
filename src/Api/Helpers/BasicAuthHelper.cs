using System.Text;

namespace Defra.PhaImportNotifications.Api.Helpers;

public static class BasicAuthHelper
{
    public static string CreateBasicAuthHeader(string username, string password)
    {
        return $"Basic {CreateBasicAuth(username, password)}";
    }

    public static string CreateBasicAuth(string username, string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
    }

    public static (string, string) FromBasicAuth(string basicAuth)
    {
        var credentialBytes = Convert.FromBase64String(basicAuth);
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":", 2);
        return (credentials[0], credentials[1]);
    }
}
