using Microsoft.AspNetCore.Builder;

namespace PhaImportNotifications.Tests.Config;

public class EnvironmentTest
{
    [Fact]
    public void IsNotDevModeByDefault()
    {
        var _builder = WebApplication.CreateBuilder();

        var isDev = PhaImportNotifications.Config.Environment.IsDevMode(_builder);

        Assert.False(isDev);
    }
}
