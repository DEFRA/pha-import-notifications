using WireMock.Server;

namespace Defra.PhaImportNotifications.Testing;

public class WireMockTestBase : IClassFixture<WireMockContext>
{
    protected WireMockServer WireMock { get; }

    protected WireMockTestBase(WireMockContext context)
    {
        WireMock = context.Server;
        WireMock.Reset();
    }
}
