using Defra.PhaImportNotifications.Api.Utils.Http;
using Serilog;
using Serilog.Core;

namespace Defra.PhaImportNotifications.Api.Tests.Utils.Http;

public class ProxyTest
{
    private readonly Logger _logger = new LoggerConfiguration().CreateLogger();

    private readonly string _proxyUri = "http://user:password@localhost:8080";
    private readonly string _localProxy = "http://localhost:8080/";
    private readonly string _localhost = "http://localhost/";

    public ProxyTest() { }

    [Fact]
    public void ExtractProxyCredentials()
    {
        var proxy = new System.Net.WebProxy { BypassProxyOnLocal = true };

        Proxy.ConfigureProxy(proxy, _proxyUri, _logger);

        var credentials = proxy.Credentials?.GetCredential(new System.Uri(_proxyUri), "Basic");

        credentials?.UserName.Should().Be("user");
        credentials?.Password.Should().Be("password");
    }

    [Fact]
    public void ExtractProxyEmptyCredentials()
    {
        var noPasswordUri = "http://user@localhost:8080";

        var proxy = new System.Net.WebProxy { BypassProxyOnLocal = true };

        Proxy.ConfigureProxy(proxy, noPasswordUri, _logger);

        proxy.Credentials.Should().BeNull();
    }

    [Fact]
    public void ExtractProxyUri()
    {
        var proxy = new System.Net.WebProxy { BypassProxyOnLocal = true };

        Proxy.ConfigureProxy(proxy, _proxyUri, _logger);
        proxy.Address.Should().NotBeNull();
        proxy.Address?.AbsoluteUri.Should().Be(_localProxy);
    }

    [Fact]
    public void CreateProxyFromUri()
    {
        var proxy = Proxy.CreateProxy(_proxyUri, _logger);

        proxy.Address.Should().NotBeNull();
        proxy.Address?.AbsoluteUri.Should().Be(_localProxy);
    }

    [Fact]
    public void CreateNoProxyFromEmptyUri()
    {
        var proxy = Proxy.CreateProxy(null, _logger);

        proxy.Address.Should().BeNull();
    }

    [Fact]
    public void ProxyShouldBypassLocal()
    {
        var proxy = Proxy.CreateProxy(_proxyUri, _logger);

        proxy.BypassProxyOnLocal.Should().BeTrue();
        proxy.IsBypassed(new Uri(_localhost)).Should().BeTrue();
        proxy.IsBypassed(new Uri("https://defra.gov.uk")).Should().BeFalse();
    }

    [Fact]
    public void HandlerShouldHaveProxy()
    {
        var handler = Proxy.CreateHttpClientHandler(_proxyUri, _logger);

        handler.Proxy.Should().NotBeNull();
        handler.UseProxy.Should().BeTrue();
        handler.Proxy?.Credentials.Should().NotBeNull();
        handler.Proxy?.GetProxy(new Uri(_localhost)).Should().NotBeNull();
        handler.Proxy?.GetProxy(new Uri("http://google.com")).Should().NotBeNull();
        handler.Proxy?.GetProxy(new Uri(_localhost))?.AbsoluteUri.Should().Be(_localhost);
        handler.Proxy?.GetProxy(new Uri("http://google.com"))?.AbsoluteUri.Should().Be(_localProxy);
    }
}
