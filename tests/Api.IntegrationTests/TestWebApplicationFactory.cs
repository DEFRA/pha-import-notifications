using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests;

public class TestWebApplicationFactory<T> : WebApplicationFactory<T>, ITestOutputHelperAccessor
    where T : class
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Lock s_lock = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(config => config.AddXUnit(this));
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // There is an issue with using CreateBootstrapLogger during host creation
        // that has started happening since the recent CDP Serilog changes have
        // been introduced. In tests, multiple hosts are created in parallel but
        // the CreateBootstrapLogger code is not thread safe and can throw errors.
        //
        // We can mitigate this issue from here by locking host creation so we
        // don't need to change host creation of the app itself.
        lock (s_lock)
            return base.CreateHost(builder);
    }

    public ITestOutputHelper? OutputHelper { get; set; }
}
