using Defra.PhaImportNotifications.BtmsStub.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.PhaImportNotifications.BtmsStub;

public static class ServiceCollectionExtensions
{
    public static void AddBtmsStub(this IServiceCollection services)
    {
        services
            .AddOptions<BtmsStubOptions>()
            .BindConfiguration("BtmsStub")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddSingleton<WireMockHostedService>();
        services.AddHostedService<WireMockHostedService>(sp => sp.GetRequiredService<WireMockHostedService>());
    }
}
