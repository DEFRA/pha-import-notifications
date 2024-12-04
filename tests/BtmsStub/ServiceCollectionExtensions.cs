using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Defra.PhaImportNotifications.BtmsStub;

public static class ServiceCollectionExtensions
{
    public static void AddBtmsStub(this IServiceCollection services, Func<IServiceProvider, bool> enabledFactory)
    {
        services.AddHostedService(sp => new WireMockHostedService(
            sp.GetRequiredService<ILogger<WireMockHostedService>>(),
            enabledFactory(sp)
        ));
    }
}
