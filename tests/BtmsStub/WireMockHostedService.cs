using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WireMock.Admin.Requests;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Defra.PhaImportNotifications.BtmsStub;

[ExcludeFromCodeCoverage]
public class WireMockHostedService(ILogger<WireMockHostedService> logger, bool startWireMock) : IHostedService
{
    private readonly WireMockServerSettings _settings = new() { Logger = new WireMockLogger(logger) };
    private WireMockServer? _wireMockServer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (startWireMock)
            _wireMockServer = WireMockServer.Start(_settings);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _wireMockServer?.Stop();

        return Task.CompletedTask;
    }

    private sealed class WireMockLogger(ILogger<WireMockHostedService> logger) : IWireMockLogger
    {
        public void Debug(string formatString, params object[] args)
        {
            logger.LogDebug(formatString, args);
        }

        public void Info(string formatString, params object[] args)
        {
            logger.LogInformation(formatString, args);
        }

        public void Warn(string formatString, params object[] args)
        {
            logger.LogWarning(formatString, args);
        }

        public void Error(string formatString, params object[] args)
        {
            logger.LogError(formatString, args);
        }

        public void Error(string formatString, Exception exception)
        {
            logger.LogError(exception, formatString, exception);
        }

        public void DebugRequestResponse(LogEntryModel logEntryModel, bool isAdminRequest)
        {
            var message = JsonSerializer.Serialize(logEntryModel, new JsonSerializerOptions { WriteIndented = true });
            logger.LogDebug("Admin[{0}] {1}", isAdminRequest, message);
        }
    }
}
