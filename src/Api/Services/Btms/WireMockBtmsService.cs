using System.Text.Json;
using WireMock.Admin.Requests;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Defra.PhaImportNotifications.Api.Services.Btms;

public class WireMockBtmsService(ILogger<WireMockBtmsService> logger)
{
    private readonly WireMockServerSettings _settings = new() { Logger = new WireMockLogger(logger), Port = 8090 };

    public void Start()
    {
        logger.LogInformation("Starting WireMock BTMS Service");
        WireMockServer.Start(_settings);
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
