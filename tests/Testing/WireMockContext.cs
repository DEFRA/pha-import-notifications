using WireMock.Server;
using WireMock.Settings;

namespace Defra.PhaImportNotifications.Testing;

public sealed class WireMockContext : IDisposable
{
    public WireMockServer Server { get; }
    public HttpClient HttpClient { get; }

    public WireMockContext()
    {
        Server = WireMockServer.Start(
            new WireMockServerSettings
            {
                // Uncomment if debugging /__admin/requests is needed
                // StartAdminInterface = true,
            }
        );
        HttpClient = new HttpClient { BaseAddress = new Uri(Server.Urls[0]) };
    }

    public void Dispose()
    {
        Server.Stop();
        Server.Dispose();
        HttpClient.Dispose();
    }
}
