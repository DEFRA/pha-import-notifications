using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Defra.PhaImportNotifications.BtmsStub.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WireMock.Admin.Requests;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Defra.PhaImportNotifications.BtmsStub;

[ExcludeFromCodeCoverage]
public class WireMockHostedService(IOptions<BtmsStubOptions> options, ILogger<WireMockHostedService> logger)
    : IHostedService
{
    private readonly WireMockServerSettings _settings = new()
    {
        Port = options.Value.Port,
        Logger = new WireMockLogger(logger),
    };
    private WireMockServer? _wireMockServer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (options.Value.Enabled)
        {
            _wireMockServer = WireMockServer.Start(_settings);

            logger.LogInformation("Started on port {0}", _settings.Port);

            // We will have methods for each scenario in the future but for
            // now these are just the ones used in our integration tests.
            _wireMockServer.StubSingleImportNotification();
            _wireMockServer.StubSingleImportNotification("CHEDA.GB.2024.fail", shouldFail: true);
            _wireMockServer.StubImportNotificationUpdates();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_wireMockServer is not null)
        {
            _wireMockServer.Stop();

            logger.LogInformation("Stopped");
        }

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

        public void Error(string message, Exception exception)
        {
            logger.LogError(exception, message, exception);
        }

        public void DebugRequestResponse(LogEntryModel logEntryModel, bool isAdminRequest)
        {
            var message = JsonSerializer.Serialize(logEntryModel, new JsonSerializerOptions { WriteIndented = true });
            logger.LogDebug("Admin[{0}] {1}", isAdminRequest, message);
        }
    }
}
