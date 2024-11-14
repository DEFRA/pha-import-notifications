using Microsoft.AspNetCore.Builder;

namespace PhaImportNotifications.Tests.Config;

public class EnvironmentTest
{
    [Fact]
    public void IsNotDevModeByDefault()
    {
        var builder = WebApplication.CreateBuilder();

        var isDev = PhaImportNotifications.Config.Environment.IsDevMode(builder);

        Assert.False(isDev);
    }
}
