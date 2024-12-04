using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.Extensions.Options;
using WireMock.Admin.Requests;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

[ExcludeFromCodeCoverage]
public class WireMockBtmsService(IOptions<BtmsOptions> btmsOptions, ILogger<WireMockBtmsService> logger)
    : IHostedService
{
    private readonly BtmsOptions _options = btmsOptions.Value;
    private readonly WireMockServerSettings _settings = new() { Logger = new WireMockLogger(logger), Port = 8090 };
    private WireMockServer? _wireMockServer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_options.StubEnabled)
            return Task.CompletedTask;

        logger.LogInformation("Starting BTMS WireMock server on http://localhost:{Port}", _settings.Port);
        _wireMockServer = WireMockServer.Start(_settings);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _wireMockServer?.Stop();
        return Task.CompletedTask;
    }

    private sealed class WireMockLogger(ILogger<WireMockBtmsService> wiremockLogger) : IWireMockLogger
    {
        public void Debug(string formatString, params object[] args)
        {
            wiremockLogger.LogDebug(formatString, args);
        }

        public void Info(string formatString, params object[] args)
        {
            wiremockLogger.LogInformation(formatString, args);
        }

        public void Warn(string formatString, params object[] args)
        {
            wiremockLogger.LogWarning(formatString, args);
        }

        public void Error(string formatString, params object[] args)
        {
            wiremockLogger.LogError(formatString, args);
        }

        public void Error(string message, Exception exception)
        {
            wiremockLogger.LogError(exception, message);
        }

        public void DebugRequestResponse(LogEntryModel logEntryModel, bool isAdminRequest)
        {
            var message = JsonSerializer.Serialize(logEntryModel, new JsonSerializerOptions { WriteIndented = true });
            wiremockLogger.LogDebug("Admin[{0}] {1}", isAdminRequest, message);
        }
    }
}
