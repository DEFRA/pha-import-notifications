using Defra.PhaImportNotifications.Api.Configuration;

namespace Defra.PhaImportNotifications.Api.Tests.Configuration;

public class BtmsOptionsTests
{
    [Fact]
    public void BasicAuthCredential_ReturnsEncodedCredentials()
    {
        var btmsOptions = new BtmsOptions
        {
            BaseUrl = "https://test",
            Password = "password",
            Username = "username",
        };

        btmsOptions.BasicAuthCredential.Should().Be("dXNlcm5hbWU6cGFzc3dvcmQ=");
    }
}
