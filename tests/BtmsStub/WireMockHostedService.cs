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
public class WireMockHostedService(ILogger<WireMockHostedService> logger) : IHostedService
{
    private readonly WireMockServerSettings _settings = new() { Logger = new WireMockLogger(logger) };

    private WireMockServer? _wireMockServer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _wireMockServer = WireMockServer.Start(_settings);

        logger.LogInformation("Started on port {Port}", _settings.Port);

        ConfigureStubbedData();

        return Task.CompletedTask;
    }

    private void ConfigureStubbedData()
    {
        // NB. import notifications returned by the following do not exist in the stub and will 404
        _wireMockServer?.StubImportNotificationUpdates();

        _wireMockServer?.StubSingleImportNotification(chedReferenceNumber: ChedReferenceNumbers.ChedA);
        _wireMockServer?.StubSingleImportNotification(chedReferenceNumber: ChedReferenceNumbers.ChedD);
        _wireMockServer?.StubSingleImportNotification(chedReferenceNumber: ChedReferenceNumbers.ChedP);
        _wireMockServer?.StubSingleImportNotification(chedReferenceNumber: ChedReferenceNumbers.ChedPP);

        _wireMockServer?.StubSingleImportNotification(shouldFail: true, chedReferenceNumber: "CHEDA.GB.2024.fail");

        // The following are linked, ChedAWithMovement has relations for Movement1 and Movement2 allowing
        // the full import notification to be retrieved
        _wireMockServer?.StubSingleImportNotification(chedReferenceNumber: ChedReferenceNumbers.ChedAWithMovement);
        _wireMockServer?.StubSingleMovement(mrn: MovementReferenceNumbers.Movement1);
        _wireMockServer?.StubSingleMovement(mrn: MovementReferenceNumbers.Movement2);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_wireMockServer is null)
            return Task.CompletedTask;

        _wireMockServer.Stop();

        logger.LogInformation("Stopped");

        return Task.CompletedTask;
    }

    [SuppressMessage("Usage", "CA2254:Template should be a static expression")]
    private sealed class WireMockLogger(ILogger<WireMockHostedService> logger) : IWireMockLogger
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

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
            var message = JsonSerializer.Serialize(logEntryModel, _jsonSerializerOptions);
            logger.LogDebug("Admin[{IsAdminRequest}] {Message}", isAdminRequest, message);
        }
    }
}
