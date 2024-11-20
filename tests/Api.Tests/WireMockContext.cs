using WireMock.Server;

namespace Defra.PhaImportNotifications.Api.Tests;

public sealed class WireMockContext : IDisposable
{
    public WireMockServer Server { get; }
    public HttpClient HttpClient { get; }

    public WireMockContext()
    {
        Server = WireMockServer.Start();
        HttpClient = new HttpClient { BaseAddress = new Uri(Server.Urls[0]) };
    }

    public void Dispose()
    {
        Server.Stop();
        Server.Dispose();
        HttpClient.Dispose();
    }
}
