using MartinCostello.Logging.XUnit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Defra.PhaImportNotifications.Api.IntegrationTests;

public class TestWebApplicationFactory<T> : WebApplicationFactory<T>, ITestOutputHelperAccessor
    where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(config => config.AddXUnit(this));
        builder.UseSetting("integrationTest", "true");
    }

    public ITestOutputHelper? OutputHelper { get; set; }
}
